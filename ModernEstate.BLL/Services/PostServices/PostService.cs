using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.BLL.JWTServices;
using ModernEstate.BLL.Services.AccountServices;
using ModernEstate.BLL.Services.AddressServices;
using ModernEstate.BLL.Services.ContactServices;
using ModernEstate.BLL.Services.HistoryServices;
using ModernEstate.BLL.Services.PostPackageServices;
using ModernEstate.BLL.Services.PropertyServices;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.Common.srcs;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.PostServices
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PostService> _logger;
        private readonly IContactService _contactService;
        private readonly IPropertyService _propertyService;
        private readonly IAddressService _addressService;
        private readonly IAccountService _accountService;
        private readonly IJwtService _jwtService;
        private readonly IHistoryService _historyService;
        private readonly IPostPackageService _postPackageService;
        private readonly Utils _utils;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PostService> logger,
                           IContactService contactService, Utils utils, IPropertyService propertyService,
                           IAddressService addressService, IAccountService accountService,
                           IJwtService jwtService, IHistoryService historyService,
                           IPostPackageService postPackageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _contactService = contactService;
            _utils = utils;
            _propertyService = propertyService;
            _addressService = addressService;
            _accountService = accountService;
            _jwtService = jwtService;
            _historyService = historyService;
            _postPackageService = postPackageService;
        }

        public async Task<Guid> CreatePost(PostRequest request)
        {
            try
            {
                Guid conactId = await _contactService.GetOrCreateAsync(request.Contact);
                if (conactId == Guid.Empty) throw new AppException(ErrorCode.NOT_NULL, "Thông tin liên hệ không được bỏ trống.");
                if (request.NewProperty == null || request.NewProperty.Address == null)
                {
                    throw new AppException(ErrorCode.NOT_NULL, "Property or AddressRequest cannot be null.");
                }
                var package = await _unitOfWork.Packages.GetByIdAsync(request.PostPackagesRequest.PackageId);
                if (package == null) throw new AppException(ErrorCode.NOT_FOUND, "Package ko tồn tại");
                var addressExisting = await _addressService.GetOrCreateAsync(request.NewProperty.Address);
                if (addressExisting == null)
                {
                    addressExisting = _mapper.Map<Address>(request.NewProperty.Address);
                    addressExisting.Id = Guid.NewGuid();
                    await _unitOfWork.Addresses.CreateAsync(addressExisting);
                }
                var property = await CreateProperty(request.NewProperty, addressExisting, request.Demand, package);
                if (property == null) throw new AppException(ErrorCode.NOT_NULL, "Property creation failed.");
                var post = _mapper.Map<Post>(request);
                post.ContactId = conactId;
                post.PropertyId = property.Id;
                post.PriorityStatus = package.PriorityStatus;
                post.SourceStatus = EnumSourceStatus.WAIT_PAYMENT;
                post.Status = EnumStatus.INACTIVE;
                post.Code = await _utils.GenerateUniqueBrokerCodeAsync("POST_");
                if (string.IsNullOrEmpty(post.Code))
                    throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR, "Post code generation failed.");
                await _unitOfWork.Posts.CreateAsync(post);
                var postPackage = await _postPackageService.CreateAsync(request.PostPackagesRequest, post.Id);
                if (postPackage == null) throw new AppException(ErrorCode.NOT_NULL, "Post package creation failed.");
                post.PostPackages.Add(postPackage);
                History history = await setupHistory(EnumHistoryChangeType.INSERT, property.Id, "Create property");
                var image = await setupImage(request.NewProperty.Images, property.Id);
                property.PropertyImages = image;

                await _unitOfWork.SaveChangesWithTransactionAsync();
                _logger.LogInformation("Post created successfully with ID: {Id}", post.Id);
                return post.PostPackages.FirstOrDefault()?.Id ?? Guid.Empty;
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, "AppException occurred: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        private async Task<Property> CreateProperty(PropertyRequest propertyRequest, Address address, EnumDemand demand, Package package)
        {

            var property = _mapper.Map<Property>(propertyRequest);
            if (propertyRequest.ProjectId != null)
            {
                var project = await _unitOfWork.Projects.GetByIdAsync(propertyRequest.ProjectId.Value);
                if (project == null) throw new AppException(ErrorCode.NOT_FOUND, "Project not found.");
                property.ProjectId = project.Id;
            }
            else
            {
                property.ProjectId = null;
            }
            property.AddressId = address.Id;
            property.Address = address;
            property.Demand = demand;
            property.Status = EnumStatusProperty.INACTIVE;
            property.StatusSource = EnumSourceStatus.WAIT_PAYMENT;
            property.PriorityStatus = package.PriorityStatus;
            property.Code = await _utils.GenerateUniqueBrokerCodeAsync("PRO_");
            if (string.IsNullOrEmpty(property.Code))
                throw new AppException(ErrorCode.NOT_NULL, "Property code generation failed.");
            await _unitOfWork.Properties.CreateAsync(property);
            return property;
        }


        private async Task<History> setupHistory(EnumHistoryChangeType type, Guid id, string reason)
        {
            string currentId = _jwtService.GetAccountId();
            if (currentId == null) throw new AppException(ErrorCode.NOT_NULL);
            var historyEntity = new History
            {
                TypeHistory = type,
                ReasonChange = reason,
                PropertyId = id,
                ChangeBy = currentId,
            };
            var history = await _historyService.CreateHistory(historyEntity);
            return history;
        }

        private async Task<List<Image>> setupImage(List<ImageRequest> imageRequests, Guid id)
        {
            var listImage = new List<Image>();
            foreach (var imageReq in imageRequests)
            {
                var imageEntity = _mapper.Map<Image>(imageReq);
                imageEntity.PropertyId = id;
                await _unitOfWork.Images.CreateAsync(imageEntity);
                listImage.Add(imageEntity);
            }
            return listImage;
        }

        public async Task<bool> DeletePost(Guid id)
        {
            try
            {
                var postExisting = await _unitOfWork.Posts.GetByIdAsync(id);
                if (postExisting == null) throw new AppException(ErrorCode.NOT_FOUND);
                await _unitOfWork.Posts.DeleteAsync(postExisting);
                await _unitOfWork.SaveChangesWithTransactionAsync();
                return true;
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, "AppException occurred: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<PostResponse?> GetByCode(string code)
        {
            try
            {
                var postExisting = await _unitOfWork.Posts.FindByCode(code);
                if (postExisting == null) throw new AppException(ErrorCode.NOT_FOUND);
                return _mapper.Map<PostResponse>(postExisting);
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, "AppException occurred: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<PostResponse?> GetById(Guid id)
        {
            try
            {
                var postExisting = await _unitOfWork.Posts.FindById(id);
                if (postExisting == null) throw new AppException(ErrorCode.NOT_FOUND);
                var post = _mapper.Map<PostResponse>(postExisting);
                if (Guid.TryParse(postExisting.PostBy, out var createdById))
                {
                    var createdBy = await _unitOfWork.Accounts.FindById(createdById);
                    post.CreatedByUser = _mapper.Map<AccountResponse>(createdBy);
                }
                if (Guid.TryParse(postExisting.AppRovedBy, out var approvedById))
                {
                    var approvedBy = await _unitOfWork.Accounts.FindById(approvedById);
                    post.ApprovedByUser = _mapper.Map<AccountResponse>(approvedBy);
                }
                return post;
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, "AppException occurred: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<PageResult<PostResponse>> GetPosts(EnumDemand? demand, EnumSourceStatus? srcStatus, Guid? postBy, EnumStatus? status,
                                                            Guid? approveBy, EnumPriorityStatus? priority, int pageCurrent, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.Posts.FindWithParams(demand, srcStatus, postBy, status, approveBy, priority);
                if (result == null) throw new AppException(ErrorCode.LIST_EMPTY);
                var pagedResult = result.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
                var total = result.Count();
                var data = new List<PostResponse>();

                foreach (var postEntity in pagedResult)
                {
                    var post = _mapper.Map<PostResponse>(postEntity);
                    if (Guid.TryParse(postEntity.PostBy, out var createdById))
                    {
                        var createdBy = await _unitOfWork.Accounts.FindById(createdById);
                        post.CreatedByUser = _mapper.Map<AccountResponse>(createdBy);
                    }
                    if (Guid.TryParse(postEntity.AppRovedBy, out var approvedById))
                    {
                        var approvedBy = await _unitOfWork.Accounts.FindById(approvedById);
                        post.ApprovedByUser = _mapper.Map<AccountResponse>(approvedBy);
                    }
                    data.Add(post);
                }

                if (data == null || !data.Any()) throw new AppException(ErrorCode.LIST_EMPTY);
                var pageResult = new PageResult<PostResponse>(data, pageSize, pageCurrent, total);
                return pageResult;
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, "AppException occurred: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> UpdatePost(Guid id, UpdatePostRequest request, bool approval)
        {
            try
            {
                var postExisting = await _unitOfWork.Posts.FindById(id);
                if (postExisting == null)
                    throw new AppException(ErrorCode.NOT_FOUND, "Post not found.");
                var property = await _unitOfWork.Properties.FindById(postExisting.PropertyId);
                if (property == null)
                    throw new AppException(ErrorCode.NOT_FOUND, "Property not found.");
                var accountCurrentId = _jwtService.GetAccountIdGuid();
                if (accountCurrentId == Guid.Empty)
                    throw new AppException(ErrorCode.NOT_NULL, "Account ID cannot be null.");
                var accountCurrent = await _unitOfWork.Accounts.FindById(accountCurrentId);
                if (accountCurrent == null)
                    throw new AppException(ErrorCode.NOT_FOUND, "Account not found.");
                if (accountCurrent?.Role?.RoleName != EnumRoleName.ROLE_STAFF ||
                    accountCurrent?.Role?.RoleName != EnumRoleName.ROLE_ADMIN ||
                    accountCurrent.Id.ToString() != postExisting.PostBy)
                    throw new AppException(ErrorCode.FORBIDDEN);
                if (approval)
                {
                    postExisting.AppRovedBy = accountCurrent.Id.ToString();
                    if (request.SourceStatus == EnumSourceStatus.APPROVE)
                    {
                        postExisting.SourceStatus = EnumSourceStatus.APPROVE;
                        postExisting.Status = EnumStatus.ACTIVE;
                        property.StatusSource = EnumSourceStatus.APPROVE;
                    }
                    else if (request.SourceStatus == EnumSourceStatus.REJECT || request.SourceStatus == EnumSourceStatus.BLOCKED)
                    {
                        postExisting.SourceStatus = request.SourceStatus ?? postExisting.SourceStatus;
                        postExisting.Status = EnumStatus.INACTIVE;
                        postExisting.RejectionReason = request.RejectionReason;
                        property.StatusSource = request.SourceStatus ?? postExisting.SourceStatus;
                    }
                }
                else
                {

                }
                await _unitOfWork.Properties.UpdateAsync(property);
                _logger.LogInformation("Property with ID {Id} approved successfully.", id);
                await _unitOfWork.Posts.UpdateAsync(postExisting);
                _logger.LogInformation("Post with ID {Id} approved successfully.", id);
                return true;
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, "AppException occurred: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> ConfirmPost(Guid id, ConfirmPostRequest request)
        {
            try
            {
                var curentAccount = _jwtService.GetAccountIdGuid();
                var postExisting = await _unitOfWork.Posts.FindById(id);
                if (postExisting == null)
                    throw new AppException(ErrorCode.NOT_FOUND, "Post not found.");
                var property = await _unitOfWork.Properties.FindById(postExisting.PropertyId);
                if (property == null)
                    throw new AppException(ErrorCode.NOT_FOUND, "Property not found.");
                var accountCurrentId = _jwtService.GetAccountIdGuid();
                if (accountCurrentId == Guid.Empty)
                    throw new AppException(ErrorCode.NOT_NULL, "Account ID cannot be null.");
                var accountCurrent = await _unitOfWork.Accounts.FindById(accountCurrentId);
                if (accountCurrent == null)
                    throw new AppException(ErrorCode.USER_NOT_FOUND, "Account not found or no permisstion");
                if (accountCurrent?.Role?.RoleName != EnumRoleName.ROLE_STAFF)
                    throw new AppException(ErrorCode.UNAUTHORIZED);
                postExisting.AppRovedBy = accountCurrent.Id.ToString();
                if (request.SourceStatus == EnumSourceStatus.APPROVE)
                {
                    postExisting.SourceStatus = EnumSourceStatus.APPROVE;
                    postExisting.Status = EnumStatus.ACTIVE;
                    property.StatusSource = EnumSourceStatus.APPROVE;
                }
                else if (request.SourceStatus == EnumSourceStatus.REJECT || request.SourceStatus == EnumSourceStatus.BLOCKED)
                {
                    postExisting.SourceStatus = request.SourceStatus;
                    postExisting.Status = EnumStatus.INACTIVE;
                    postExisting.RejectionReason = request.RejectionReason;
                    property.StatusSource = request.SourceStatus;
                }
                await _unitOfWork.Posts.UpdateAsync(postExisting);
                await _unitOfWork.Properties.UpdateAsync(property);
                await _unitOfWork.SaveChangesWithTransactionAsync();
                return true;
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, "AppException occurred: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<PageResult<PostResponse>> GetPostConfirm(int pageCurrent, int pageSize)
        {
            try
            {
                DateTime now = DateTime.Now;
                var result = await _unitOfWork.Posts.FindByConfirm(EnumSourceStatus.WAIT_APPROVE, now);
                if (result == null) throw new AppException(ErrorCode.LIST_EMPTY);
                var pagedResult = result.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
                var total = result.Count();
                var data = _mapper.Map<List<PostResponse>>(pagedResult);
                if (data == null || !data.Any()) throw new AppException(ErrorCode.LIST_EMPTY);
                var pageResult = new PageResult<PostResponse>(data, pageSize, pageCurrent, total);
                return pageResult;
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, "AppException occurred: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }
    }
}

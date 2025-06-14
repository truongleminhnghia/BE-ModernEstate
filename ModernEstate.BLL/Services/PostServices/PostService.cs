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
                var addressExisting = await _addressService.GetOrCreateAsync(request.NewProperty.Address);
                if (addressExisting == null)
                {
                    addressExisting = _mapper.Map<Address>(request.NewProperty.Address);
                    addressExisting.Id = Guid.NewGuid();
                    await _unitOfWork.Addresses.CreateAsync(addressExisting);
                }
                var property = await CreateProperty(request.NewProperty, addressExisting, request.Demand);
                if (property == null) throw new AppException(ErrorCode.NOT_NULL, "Property creation failed.");
                var post = _mapper.Map<Post>(request);
                post.ContactId = conactId;
                post.PropertyId = property.Id;
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

        private async Task<Property> CreateProperty(PropertyRequest propertyRequest, Address address, EnumDemand demand)
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

        public async Task<PageResult<PostResponse>> GetPosts(string? title, EnumStatePost? state, EnumSourceStatus? srcStatus, int pageCurrent, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.Posts.FindWithParams(title, state, srcStatus);
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

        public Task<bool> UpdatePost(Guid id, UpdatePostRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

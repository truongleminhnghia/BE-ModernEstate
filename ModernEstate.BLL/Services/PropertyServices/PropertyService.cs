
using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.BLL.JWTServices;
using ModernEstate.BLL.Services.AccountServices;
using ModernEstate.BLL.Services.AddressServices;
using ModernEstate.BLL.Services.HistoryServices;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.Common.srcs;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.PropertyServices
{
    public class PropertyService : IPropertyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PropertyService> _logger;
        private readonly IAddressService _addressService;
        private readonly IAccountService _accountService;
        private readonly Utils _utils;
        private readonly IJwtService _jwtService;
        private readonly IHistoryService _historyService;

        public PropertyService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PropertyService> logger,
                                IAddressService addressService, IAccountService accountService,
                                Utils utils, IJwtService jwtService, IHistoryService historyService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _addressService = addressService;
            _accountService = accountService;
            _utils = utils;
            _jwtService = jwtService;
            _historyService = historyService;
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PropertyResponse?> GetByCode(string code)
        {
            try
            {
                var propertyExisting = await _unitOfWork.Properties.FindByCode(code);
                if (propertyExisting == null) throw new AppException(ErrorCode.NOT_FOUND);
                return _mapper.Map<PropertyResponse>(propertyExisting);
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

        public async Task<PropertyResponse?> GetById(Guid id)
        {
            try
            {
                var propertyExisting = await _unitOfWork.Properties.FindById(id);
                if (propertyExisting == null) throw new AppException(ErrorCode.NOT_FOUND);
                return _mapper.Map<PropertyResponse>(propertyExisting);
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

        public async Task<PageResult<PropertyResponse>> GetProperties(string? Title, double? minPrice, double? maxPrice,
                                                                EnumTypeProperty? typeProperty, float? minArea,
                                                                float? maxArea, int? numberOfBedroom, int? numberOfBathroom,
                                                                int? numberOfFloor, int? numberOfRoom, EnumStateProperty? state,
                                                                EnumStatusProperty? status, int pageCurrent, int pageSize
        )
        {
            try
            {
                var result = await _unitOfWork.Properties.FindProperties(Title, minPrice, maxPrice, typeProperty, minArea,
                                                                        maxArea, numberOfBedroom, numberOfBathroom, numberOfFloor,
                                                                        numberOfRoom, state, status);
                if (result == null) throw new AppException(ErrorCode.LIST_EMPTY);
                var pagedResult = result.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
                var total = result.Count();
                var data = _mapper.Map<List<PropertyResponse>>(pagedResult);
                if (data == null || !data.Any()) throw new AppException(ErrorCode.LIST_EMPTY);
                var pageResult = new PageResult<PropertyResponse>(data, pageSize, pageCurrent, total);
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

        public async Task<Property> Save(PropertyRequest request)
        {
            try
            {
                Guid accountId = Guid.Empty;
                var propertyExisting = await _unitOfWork.Properties.FindByTitle(request.Title);
                if (propertyExisting != null) throw new AppException(ErrorCode.HAS_EXISTED);
                // var addressExisting = await _addressService.GetOrCreateAsync(request.AddressRequest);
                // if (addressExisting == null)
                // {
                // addressExisting = _mapper.Map<Address>(request.AddressRequest);
                // addressExisting.Id = Guid.NewGuid();
                // await _unitOfWork.Addresses.CreateAsync(addressExisting);
                // }
                // var accountExisting = await _unitOfWork.Accounts.FindByPhone(request.OwnerPropertyRequest.PhoneNumer);
                // if (accountExisting == null)
                // {
                AccountRequest accountRequest = new AccountRequest
                {
                    // Email = request.OwnerPropertyRequest.Email,
                    // FirstName = request.OwnerPropertyRequest.FisrtName,
                    // LastName = request.OwnerPropertyRequest.LastName,
                    Password = "123@123",
                    RoleName = EnumRoleName.ROLE_PROPERTY_OWNER,
                    EnumAccountStatus = EnumAccountStatus.ACTIVE,
                };
                // accountId = await _accountService.CreateAccountBrokerOrOwner(accountRequest);
                // }
                // else
                // {
                // if (!accountExisting.Role.RoleName.Equals(EnumRoleName.ROLE_PROPERTY_OWNER))
                // {
                // accountExisting.Role.RoleName = EnumRoleName.ROLE_PROPERTY_OWNER;
                // await _unitOfWork.Accounts.UpdateAsync(accountExisting);
                // }
                // accountId = accountExisting.OwnerProperty.Id;
                // }
                // var property = _mapper.Map<Property>(request);
                // property.AddressId = addressExisting.Id;
                // property.Address = addressExisting;
                // property.Code = await _utils.GenerateUniqueBrokerCodeAsync("PRO_");
                // property.OwnerId = accountId;
                // await _unitOfWork.Properties.CreateAsync(property);
                // History history = await setupHistory(EnumHistoryChangeType.INSERT, property.Id, "Create property");
                // var image = await setupImage(request.PropertyImages, property.Id);
                // property.PropertyImages = image;
                // await _unitOfWork.SaveChangesWithTransactionAsync();
                // return _mapper.Map<PropertyResponse>(property);
                // return property;
                return null;
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

        public Task<bool> Update(Guid id, UpdatePropertyRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
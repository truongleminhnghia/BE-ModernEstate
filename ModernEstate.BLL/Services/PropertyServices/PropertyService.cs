
using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;

namespace ModernEstate.BLL.Services.PropertyServices
{
    public class PropertyService : IPropertyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PropertyService> _logger;

        public PropertyService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PropertyService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
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

        public Task<bool> Save(PropertyRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Guid id, UpdatePropertyRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
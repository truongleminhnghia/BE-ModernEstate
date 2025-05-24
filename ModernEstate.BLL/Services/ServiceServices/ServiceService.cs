using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.ServiceServices
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceService> _logger;

        public ServiceService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<ServiceService> logger
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ServiceResponse>> GetAllAsync()
        {
            try
            {
                var entities = await _unitOfWork.Services.GetAllAsync();
                return _mapper.Map<IEnumerable<ServiceResponse>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all services");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<ServiceResponse> CreateAsync(ServiceRequest request)
        {
            try
            {
                var entity = _mapper.Map<Service>(request);
                entity.Id = Guid.NewGuid();

                await _unitOfWork.Services.CreateAsync(entity);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return _mapper.Map<ServiceResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create service");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<ServiceResponse?> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.Services.GetByIdAsync(id);
                if (entity == null)
                    return null;
                return _mapper.Map<ServiceResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get service by id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> UpdateAsync(Guid id, ServiceRequest request)
        {
            try
            {
                var entity = await _unitOfWork.Services.GetByIdAsync(id);
                if (entity == null)
                    return false;

                _mapper.Map(request, entity);
                _unitOfWork.Services.Update(entity);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update service id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.Services.GetByIdAsync(id);
                if (entity == null)
                    return false;

                _unitOfWork.Services.Delete(entity);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete service id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<PageResult<ServiceResponse>> GetWithParamsAsync(
            EnumTypeService? serviceType,
            int pageCurrent,
            int pageSize
        )
        {
            try
            {
                var all = await _unitOfWork.Services.FindServicesAsync(serviceType);
                if (!all.Any())
                    throw new AppException(ErrorCode.LIST_EMPTY);

                var paged = all.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();

                var dtos = _mapper.Map<System.Collections.Generic.List<ServiceResponse>>(paged);
                return new PageResult<ServiceResponse>(dtos, pageSize, pageCurrent, all.Count());
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get services with params");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }
    }
}

using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Exceptions;
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
                var Services = await _unitOfWork.Services.GetAllAsync();
                return _mapper.Map<IEnumerable<ServiceResponse>>(Services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all services");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<ServiceResponse?> GetByIdAsync(Guid id)
        {
            try
            {
                var Services = await _unitOfWork.Services.GetByIdAsync(id);
                if (Services == null)
                    return null;
                return _mapper.Map<ServiceResponse>(Services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get service by id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<ServiceResponse> CreateAsync(ServiceRequest request)
        {
            try
            {
                var Services = _mapper.Map<Service>(request);
                Services.Id = Guid.NewGuid();

                await _unitOfWork.Services.CreateAsync(Services);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return _mapper.Map<ServiceResponse>(Services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create service");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> UpdateAsync(Guid id, ServiceRequest request)
        {
            try
            {
                var Services = await _unitOfWork.Services.GetByIdAsync(id);
                if (Services == null)
                    return false;

                _mapper.Map(request, Services);

                _unitOfWork.Services.Update(Services);
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
                var Services = await _unitOfWork.Services.GetByIdAsync(id);
                if (Services == null)
                    return false;

                _unitOfWork.Services.Delete(Services);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete service id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }
    }
}

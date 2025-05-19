using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.ProvideServices
{
    public class ProvideService : IProvideService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProvideService> _logger;

        public ProvideService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<ProvideService> logger
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ProvideResponse>> GetAllAsync()
        {
            try
            {
                var entities = await _unitOfWork.Provides.GetAllAsync();
                return _mapper.Map<IEnumerable<ProvideResponse>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all provides");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<ProvideResponse?> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.Provides.GetByIdAsync(id);
                if (entity == null)
                    return null;
                return _mapper.Map<ProvideResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get provide by id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<ProvideResponse> CreateAsync(ProvideRequest request)
        {
            try
            {
                var entity = _mapper.Map<Provide>(request);
                entity.Id = Guid.NewGuid();

                await _unitOfWork.Provides.CreateAsync(entity);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return _mapper.Map<ProvideResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create provide");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> UpdateAsync(Guid id, ProvideRequest request)
        {
            try
            {
                var entity = await _unitOfWork.Provides.GetByIdAsync(id);
                if (entity == null)
                    return false;

                _mapper.Map(request, entity);

                _unitOfWork.Provides.Update(entity);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update provide id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.Provides.GetByIdAsync(id);
                if (entity == null)
                    return false;

                _unitOfWork.Provides.Delete(entity);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete provide id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }
    }
}

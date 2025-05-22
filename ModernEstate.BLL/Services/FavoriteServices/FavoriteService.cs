using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.FavoriteServices
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<FavoriteService> _logger;

        public FavoriteService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<FavoriteService> logger
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<FavoriteResponse>> GetAllAsync()
        {
            try
            {
                var entities = await _unitOfWork.Favorites.GetAllAsync();
                return _mapper.Map<IEnumerable<FavoriteResponse>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all favorites");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<FavoriteResponse?> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.Favorites.GetByIdAsync(id);
                if (entity == null)
                    return null;
                return _mapper.Map<FavoriteResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get favorite by id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<FavoriteResponse> CreateAsync(FavoriteRequest request)
        {
            try
            {
                var entity = _mapper.Map<Favorite>(request);
                entity.Id = Guid.NewGuid();

                await _unitOfWork.Favorites.CreateAsync(entity);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return _mapper.Map<FavoriteResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create favorite");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> UpdateAsync(Guid id, FavoriteRequest request)
        {
            try
            {
                var entity = await _unitOfWork.Favorites.GetByIdAsync(id);
                if (entity == null)
                    return false;

                _mapper.Map(request, entity);

                _unitOfWork.Favorites.Update(entity);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update favorite id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.Favorites.GetByIdAsync(id);
                if (entity == null)
                    return false;

                _unitOfWork.Favorites.Delete(entity);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete favorite id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }
    }
}

using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.FavoriteServices
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<FavoriteService> _logger;

        public FavoriteService(IUnitOfWork uow, IMapper mapper, ILogger<FavoriteService> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PageResult<FavoriteResponse>> GetAllAsync(Guid? accountId, Guid? propertyId, int pageCurrent, int pageSize)
        {
            try
            {
                var result = await _uow.Favorites.FindWithParams(accountId, propertyId);
                if (result == null) throw new AppException(ErrorCode.LIST_EMPTY);
                var pagedResult = result.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
                var total = result.Count();
                var data = _mapper.Map<List<FavoriteResponse>>(pagedResult);
                if (data == null || !data.Any()) throw new AppException(ErrorCode.LIST_EMPTY);
                var pageResult = new PageResult<FavoriteResponse>(data, pageSize, pageCurrent, total);
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

        public async Task<FavoriteResponse?> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _uow.Favorites.FindById(id);
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
                var accountExisting = await _uow.Accounts.FindById(request.AccountId);
                if (accountExisting == null) throw new AppException(ErrorCode.USER_NOT_FOUND);
                var entity = _mapper.Map<Favorite>(request);
                entity.Id = Guid.NewGuid();

                await _uow.Favorites.CreateAsync(entity);
                await _uow.SaveChangesWithTransactionAsync();

                return _mapper.Map<FavoriteResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create favorite");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        // public async Task<FavoriteResponse?> GetByIdAsync(Guid id)
        // {
        //     try
        //     {
        //         var entity = await _uow.Favorites.GetByIdAsync(id);
        //         if (entity == null)
        //             return null;
        //         return _mapper.Map<FavoriteResponse>(entity);
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, $"Failed to get favorite by id {id}");
        //         throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
        //     }
        // }

        public async Task<bool> UpdateAsync(Guid id, FavoriteRequest request)
        {
            try
            {
                var entity = await _uow.Favorites.GetByIdAsync(id);
                if (entity == null)
                    return false;

                _mapper.Map(request, entity);
                _uow.Favorites.Update(entity);
                await _uow.SaveChangesWithTransactionAsync();

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
                var entity = await _uow.Favorites.GetByIdAsync(id);
                if (entity == null)
                    return false;

                _uow.Favorites.Delete(entity);
                await _uow.SaveChangesWithTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete favorite id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        // public async Task<PageResult<FavoriteResponse>> GetWithParamsAsync(
        //     Guid? accountId,
        //     Guid? propertyId,
        //     int pageCurrent,
        //     int pageSize
        // )
        // {
        //     try
        //     {
        //         var all = await _uow.Favorites.FindFavoritesAsync(accountId, propertyId);
        //         if (!all.Any())
        //             throw new AppException(ErrorCode.LIST_EMPTY);

        //         var paged = all.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();

        //         var dtos = _mapper.Map<System.Collections.Generic.List<FavoriteResponse>>(paged);
        //         return new PageResult<FavoriteResponse>(dtos, pageSize, pageCurrent, all.Count());
        //     }
        //     catch (AppException)
        //     {
        //         throw;
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Failed to get favorites with params");
        //         throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
        //     }
        // }
    }
}

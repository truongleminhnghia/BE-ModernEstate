using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.NewServices
{
    public class NewService : INewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<NewService> _logger;

        public NewService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<NewService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<NewResponse>> GetAllAsync()
        {
            try
            {
                var entities = await _unitOfWork.News.GetAllAsync();
                return _mapper.Map<IEnumerable<NewResponse>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all news");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<NewResponse?> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.News.GetByIdAsync(id);
                if (entity == null)
                    return null;
                return _mapper.Map<NewResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get news by id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<NewResponse> CreateAsync(NewRequest request)
        {
            try
            {
                var entity = _mapper.Map<New>(request);
                entity.Id = Guid.NewGuid();

                await _unitOfWork.News.CreateAsync(entity);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return _mapper.Map<NewResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create news");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> UpdateAsync(Guid id, NewRequest request)
        {
            try
            {
                var entity = await _unitOfWork.News.GetByIdAsync(id);
                if (entity == null)
                    return false;

                _mapper.Map(request, entity);

                _unitOfWork.News.Update(entity);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update news id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.News.GetByIdAsync(id);
                if (entity == null)
                    return false;

                _unitOfWork.News.Delete(entity);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete news id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }
    }
}

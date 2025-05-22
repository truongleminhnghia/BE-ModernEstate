using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CategoryService> logger
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
        {
            try
            {
                var category = await _unitOfWork.Categories.GetAllAsync();
                return _mapper.Map<IEnumerable<CategoryResponse>>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all categories");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<CategoryResponse?> GetByIdAsync(Guid id)
        {
            try
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(id);
                if (category == null)
                    return null;
                return _mapper.Map<CategoryResponse>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get category by id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<CategoryResponse> CreateAsync(CategoryRequest request)
        {
            try
            {
                var category = _mapper.Map<Category>(request);
                category.Id = Guid.NewGuid();

                await _unitOfWork.Categories.CreateAsync(category);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return _mapper.Map<CategoryResponse>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create category");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> UpdateAsync(Guid id, CategoryRequest request)
        {
            try
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(id);
                if (category == null)
                    return false;

                _mapper.Map(request, category);

                _unitOfWork.Categories.Update(category);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update category id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(id);
                if (category == null)
                    return false;

                _unitOfWork.Categories.Delete(category);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete category id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }
    }
}

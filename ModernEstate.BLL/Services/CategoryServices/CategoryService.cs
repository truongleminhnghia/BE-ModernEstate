// ModernEstate.BLL.Services.CategoryServices/CategoryService.cs
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(IUnitOfWork uow, IMapper mapper, ILogger<CategoryService> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
        {
            try
            {
                var entities = await _uow.Categories.GetAllAsync();
                return _mapper.Map<IEnumerable<CategoryResponse>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all categories");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<CategoryResponse> CreateAsync(CategoryRequest request)
        {
            try
            {
                // Có thể thêm check tồn tại nếu cần, dựa vào business rule
                var entity = _mapper.Map<Category>(request);
                entity.Id = Guid.NewGuid();

                await _uow.Categories.CreateAsync(entity);
                await _uow.SaveChangesWithTransactionAsync();

                return _mapper.Map<CategoryResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create category");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<CategoryResponse?> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _uow.Categories.GetByIdAsync(id);
                if (entity == null)
                    return null;
                return _mapper.Map<CategoryResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get category by id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> UpdateAsync(Guid id, CategoryRequest request)
        {
            try
            {
                var entity = await _uow.Categories.GetByIdAsync(id);
                if (entity == null)
                    return false;

                _mapper.Map(request, entity);
                _uow.Categories.Update(entity);
                await _uow.SaveChangesWithTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update category {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await _uow.Categories.GetByIdAsync(id);
                if (entity == null)
                    return false;

                _uow.Categories.Delete(entity);
                await _uow.SaveChangesWithTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete category {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<PageResult<CategoryResponse>> GetWithParamsAsync(
            EnumCategoryName? categoryName,
            int pageCurrent,
            int pageSize
        )
        {
            try
            {
                var all = await _uow.Categories.FindCategoriesAsync(categoryName);
                if (!all.Any())
                    throw new AppException(ErrorCode.LIST_EMPTY);

                var paged = all.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();

                var dtos = _mapper.Map<System.Collections.Generic.List<CategoryResponse>>(paged);
                return new PageResult<CategoryResponse>(dtos, pageSize, pageCurrent, all.Count());
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get categories with params");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.CategoryServices;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _svc;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService svc, ILogger<CategoryController> logger)
        {
            _svc = svc;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetWithParams(
            [FromQuery] EnumCategoryName? categoryName,
            [FromQuery(Name = "page_current")] int pageCurrent = 1,
            [FromQuery(Name = "page_size")] int pageSize = 10
        )
        {
            var page = await _svc.GetWithParamsAsync(categoryName, pageCurrent, pageSize);
            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Categories retrieved successfully",
                    Data = page,
                }
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dto = await _svc.GetByIdAsync(id);
            if (dto == null)
            {
                _logger.LogWarning($"Category not found with id {id}");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Category not found",
                        Data = null,
                    }
                );
            }

            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Category retrieved successfully",
                    Data = dto,
                }
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryRequest request)
        {
            var dto = await _svc.CreateAsync(request);
            return CreatedAtAction(
                nameof(GetById),
                new { id = dto.Id },
                new ApiResponse
                {
                    Code = StatusCodes.Status201Created,
                    Success = true,
                    Message = "Category created successfully",
                    Data = dto,
                }
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CategoryRequest request)
        {
            if (!await _svc.UpdateAsync(id, request))
            {
                _logger.LogWarning($"Failed to update category with id {id} - not found");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Category not found",
                        Data = null,
                    }
                );
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _svc.DeleteAsync(id))
            {
                _logger.LogWarning($"Failed to delete category with id {id} - not found");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Category not found",
                        Data = null,
                    }
                );
            }

            return NoContent();
        }
    }
}

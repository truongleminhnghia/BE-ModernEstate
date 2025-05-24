using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.FavoriteServices;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/favorites")]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _svc;
        private readonly ILogger<FavoriteController> _logger;

        public FavoriteController(IFavoriteService svc, ILogger<FavoriteController> logger)
        {
            _svc = svc;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetWithParams(
            [FromQuery] Guid? accountId,
            [FromQuery] Guid? propertyId,
            [FromQuery(Name = "page_current")] int pageCurrent = 1,
            [FromQuery(Name = "page_size")] int pageSize = 10
        )
        {
            var page = await _svc.GetAllAsync(accountId, propertyId, pageCurrent, pageSize);
            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Favorites retrieved successfully",
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
                _logger.LogWarning($"Favorite not found with id {id}");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Favorite not found",
                        Data = null,
                    }
                );
            }

            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Favorite retrieved successfully",
                    Data = dto,
                }
            );
        }

        [HttpPost]
        [Authorize(Roles = "ROLE_STAFF, ROLE_CUSTOMER, ROLE_BROKER, ROLE_ADMIN, ROLE_PROPERTY_OWNER")]
        public async Task<IActionResult> Create([FromBody] FavoriteRequest request)
        {
            var created = await _svc.CreateAsync(request);
            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                new ApiResponse
                {
                    Code = StatusCodes.Status201Created,
                    Success = true,
                    Message = "Favorite created successfully",
                    Data = created,
                }
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] FavoriteRequest request)
        {
            if (!await _svc.UpdateAsync(id, request))
            {
                _logger.LogWarning($"Failed to update favorite with id {id} - not found");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Favorite not found",
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
                _logger.LogWarning($"Failed to delete favorite with id {id} - not found");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Favorite not found",
                        Data = null,
                    }
                );
            }
            return NoContent();
        }
    }
}

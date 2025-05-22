using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IFavoriteService _favoriteService;
        private readonly ILogger<FavoriteController> _logger;

        public FavoriteController(
            IFavoriteService favoriteService,
            ILogger<FavoriteController> logger
        )
        {
            _favoriteService = favoriteService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid? account_id,
                                                [FromQuery] int page_current = 1,
                                                [FromQuery] int page_size = 10)
        {
            var favorites = await _favoriteService.GetAllAsync(account_id, page_current, page_size);
            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Fetched favorites successfully",
                    Data = favorites,
                }
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var favorite = await _favoriteService.GetByIdAsync(id);
            if (favorite == null)
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
                    Message = "Fetched favorite successfully",
                    Data = favorite,
                }
            );
        }

        [HttpPost]
        [Authorize(Roles = "ROLE_STAFF, ROLE_CUSTOMER, ROLE_BROKER, ROLE_ADMIN, ROLE_PROPERTY_OWNER")]
        public async Task<IActionResult> Create([FromBody] FavoriteRequest request)
        {
            var created = await _favoriteService.CreateAsync(request);
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
            var success = await _favoriteService.UpdateAsync(id, request);
            if (!success)
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
            var success = await _favoriteService.DeleteAsync(id);
            if (!success)
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

using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.ProvideServices;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [Route("api/v1/provides")]
    [ApiController]
    public class ProvideController : ControllerBase
    {
        private readonly IProvideService _provideService;
        private readonly ILogger<ProvideController> _logger;

        public ProvideController(IProvideService provideService, ILogger<ProvideController> logger)
        {
            _provideService = provideService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var provides = await _provideService.GetAllAsync();
            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Fetched provides successfully",
                    Data = provides,
                }
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var provide = await _provideService.GetByIdAsync(id);
            if (provide == null)
            {
                _logger.LogWarning($"Provide not found with id {id}");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Provide not found",
                        Data = null,
                    }
                );
            }
            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Fetched provide successfully",
                    Data = provide,
                }
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProvideRequest request)
        {
            var created = await _provideService.CreateAsync(request);
            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                new ApiResponse
                {
                    Code = StatusCodes.Status201Created,
                    Success = true,
                    Message = "Provide created successfully",
                    Data = created,
                }
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProvideRequest request)
        {
            var success = await _provideService.UpdateAsync(id, request);
            if (!success)
            {
                _logger.LogWarning($"Failed to update provide with id {id} - not found");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Provide not found",
                        Data = null,
                    }
                );
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _provideService.DeleteAsync(id);
            if (!success)
            {
                _logger.LogWarning($"Failed to delete provide with id {id} - not found");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Provide not found",
                        Data = null,
                    }
                );
            }
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.NewServices;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/news")]
    public class NewController : ControllerBase
    {
        private readonly INewService _newService;
        private readonly ILogger<NewController> _logger;

        public NewController(INewService newService, ILogger<NewController> logger)
        {
            _newService = newService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var news = await _newService.GetAllAsync();
            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Fetched news successfully",
                    Data = news,
                }
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var news = await _newService.GetByIdAsync(id);
            if (news == null)
            {
                _logger.LogWarning($"News not found with id {id}");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "News not found",
                        Data = null,
                    }
                );
            }
            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Fetched news successfully",
                    Data = news,
                }
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewRequest request)
        {
            var created = await _newService.CreateAsync(request);
            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                new ApiResponse
                {
                    Code = StatusCodes.Status201Created,
                    Success = true,
                    Message = "News created successfully",
                    Data = created,
                }
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] NewRequest request)
        {
            var success = await _newService.UpdateAsync(id, request);
            if (!success)
            {
                _logger.LogWarning($"Failed to update news with id {id} - not found");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "News not found",
                        Data = null,
                    }
                );
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _newService.DeleteAsync(id);
            if (!success)
            {
                _logger.LogWarning($"Failed to delete news with id {id} - not found");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "News not found",
                        Data = null,
                    }
                );
            }
            return NoContent();
        }
    }
}

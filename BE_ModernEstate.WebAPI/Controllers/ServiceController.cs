using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.ServiceServices;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/services")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(IServiceService serviceService, ILogger<ServiceController> logger)
        {
            _serviceService = serviceService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "ROLE_MANAGER, ROLE_STAFF")]
        public async Task<IActionResult> GetWithParams(
            [FromQuery] EnumTypeService? serviceType,
            [FromQuery(Name = "page_current")] int pageCurrent = 1,
            [FromQuery(Name = "page_size")] int pageSize = 10
        )
        {
            var page = await _serviceService.GetWithParamsAsync(serviceType, pageCurrent, pageSize);
            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Services retrieved successfully",
                    Data = page,
                }
            );
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ROLE_MANAGER, ROLE_STAFF")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dto = await _serviceService.GetByIdAsync(id);
            if (dto == null)
            {
                _logger.LogWarning($"Service not found with id {id}");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Service not found",
                        Data = null,
                    }
                );
            }

            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Service retrieved successfully",
                    Data = dto,
                }
            );
        }

        [HttpPost]
        [Authorize(Roles = "ROLE_MANAGER, ROLE_STAFF")]
        public async Task<IActionResult> Create([FromBody] ServiceRequest request)
        {
            var created = await _serviceService.CreateAsync(request);
            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                new ApiResponse
                {
                    Code = StatusCodes.Status201Created,
                    Success = true,
                    Message = "Service created successfully",
                    Data = created,
                }
            );
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ROLE_MANAGER, ROLE_STAFF")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ServiceRequest request)
        {
            var success = await _serviceService.UpdateAsync(id, request);
            if (!success)
            {
                _logger.LogWarning($"Failed to update service with id {id} - not found");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Service not found",
                        Data = null,
                    }
                );
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ROLE_MANAGER, ROLE_STAFF")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _serviceService.DeleteAsync(id);
            if (!success)
            {
                _logger.LogWarning($"Failed to delete service with id {id} - not found");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Service not found",
                        Data = null,
                    }
                );
            }
            return NoContent();
        }
    }
}

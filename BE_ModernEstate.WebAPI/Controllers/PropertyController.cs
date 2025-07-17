
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.PropertyServices;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/properties")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly ILogger<PropertyController> _logger;

        public PropertyController(IPropertyService propertyService, ILogger<PropertyController> logger)
        {
            _propertyService = propertyService;
            _logger = logger;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateProperty([FromBody] PropertyRequest request)
        {
            var result = await _propertyService.Save(request);
            if (result == null)
            {
                return BadRequest(new ApiResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Success = false,
                    Message = "Property creation failed",
                    Data = null
                });
            }
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Property created successfully",
                Data = result
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var project = await _propertyService.GetById(id);
            if (project == null)
            {
                return NotFound(new ApiResponse
                {
                    Code = StatusCodes.Status404NotFound,
                    Success = false,
                    Message = "Property not found",
                    Data = null
                });
            }
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Property retrieved successfully",
                Data = project
            });
        }

        [HttpGet("")]
        public async Task<IActionResult> GetProperties([FromQuery] string? Title, [FromQuery] double? minPrice, [FromQuery] double? maxPrice,
                                                        [FromQuery] EnumTypeProperty? typeProperty, [FromQuery] float? minArea,
                                                        [FromQuery] float? maxArea, [FromQuery] int? numberOfBedroom,
                                                        [FromQuery] int? numberOfBathroom, [FromQuery] int? numberOfFloor, [FromQuery] int? numberOfRoom,
                                                        [FromQuery] EnumStateProperty? state, [FromQuery] EnumStatusProperty? status,
                                                        [FromQuery] int page_current = 1, [FromQuery] int page_size = 10)
        {
            var result = await _propertyService.GetProperties(Title, minPrice, maxPrice, typeProperty, minArea, maxArea, numberOfBedroom,
                                                              numberOfBathroom, numberOfFloor, numberOfRoom, state, status, page_current, page_size);
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Property retrieved successfully",
                Data = result
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdatePropertyRequest request)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok();
        }
    }
}
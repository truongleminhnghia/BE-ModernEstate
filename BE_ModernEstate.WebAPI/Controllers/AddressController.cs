using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.AddressServices;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/addresses")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _svc;
        private readonly ILogger<AddressController> _logger;

        public AddressController(IAddressService svc, ILogger<AddressController> logger)
        {
            _svc = svc;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "ROLE_MANAGER, ROLE_STAFF, ROLE_CUSTOMER")]
        public async Task<IActionResult> GetWithParams(
            [FromQuery] string? city,
            [FromQuery] string? district,
            [FromQuery] string? ward,
            [FromQuery(Name = "page_current")] int pageCurrent = 1,
            [FromQuery(Name = "page_size")] int pageSize = 10
        )
        {
            var page = await _svc.GetWithParamsAsync(city, district, ward, pageCurrent, pageSize);
            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Addresses retrieved successfully",
                    Data = page,
                }
            );
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ROLE_MANAGER, ROLE_STAFF, ROLE_CUSTOMER")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dto = await _svc.GetByIdAsync(id);
            if (dto == null)
            {
                _logger.LogWarning($"Address not found with id {id}");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Address not found",
                        Data = null,
                    }
                );
            }

            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Address retrieved successfully",
                    Data = dto,
                }
            );
        }

        [HttpPost]
        [Authorize(Roles = "ROLE_MANAGER, ROLE_STAFF, ROLE_CUSTOMER")]
        public async Task<IActionResult> Create([FromBody] AddressRequest request)
        {
            var created = await _svc.CreateAsync(request);
            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                new ApiResponse
                {
                    Code = StatusCodes.Status201Created,
                    Success = true,
                    Message = "Address created successfully",
                    Data = created,
                }
            );
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ROLE_MANAGER, ROLE_STAFF,  ROLE_CUSTOMER")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AddressRequest request)
        {
            if (!await _svc.UpdateAsync(id, request))
            {
                _logger.LogWarning($"Failed to update address with id {id} - not found");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Address not found",
                        Data = null,
                    }
                );
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ROLE_MANAGER, ROLE_STAFF, ROLE_CUSTOMER")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _svc.DeleteAsync(id))
            {
                _logger.LogWarning($"Failed to delete address with id {id} - not found");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Address not found",
                        Data = null,
                    }
                );
            }
            return NoContent();
        }
    }
}

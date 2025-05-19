using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.AddressServices;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [Route("api/v1/addresses")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly ILogger<AddressController> _logger;

        public AddressController(IAddressService addressService, ILogger<AddressController> logger)
        {
            _addressService = addressService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var addresses = await _addressService.GetAllAsync();
            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Fetched addresses successfully",
                    Data = addresses,
                }
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var address = await _addressService.GetByIdAsync(id);
            if (address == null)
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
                    Message = "Fetched address successfully",
                    Data = address,
                }
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddressRequest request)
        {
            var created = await _addressService.CreateAsync(request);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] AddressRequest request)
        {
            var success = await _addressService.UpdateAsync(id, request);
            if (!success)
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
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _addressService.DeleteAsync(id);
            if (!success)
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

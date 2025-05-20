using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.AccountServices;
using ModernEstate.BLL.Services.BrokerServices;
using ModernEstate.BLL.Services.OwnerPropertyServices;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.ApiResponse;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [Route("api/v1/property-owners")]
    [ApiController]
    public class PropertyOwnerController : ControllerBase
    {
        private readonly IOwnerPropertyService _ownerPropertyService;

        public PropertyOwnerController(IOwnerPropertyService ownerPropertyService)
        {
            _ownerPropertyService = ownerPropertyService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterPropertyOwner()
        {
            var userIdClaim = User.FindFirst("accountId");
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var accountId))
                throw new AppException(ErrorCode.UNAUTHORIZED);

            var result = await _ownerPropertyService.RegisterPropertyOwner(accountId);
            if (result.Success)
                return Ok(new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = result.Message
                });
            else return BadRequest(new ApiResponse
            {
                Code = StatusCodes.Status400BadRequest,
                Success = false,
                Message = result.Message
            });

        }
    }
}

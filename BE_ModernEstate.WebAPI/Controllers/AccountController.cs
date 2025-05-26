
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.JWTServices;
using ModernEstate.BLL.Services.AccountServices;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [Route("api/v1/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        private readonly IJwtService _jwtService;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger, IJwtService jwtService)
        {
            _accountService = accountService;
            _logger = logger;
            _jwtService = jwtService;
        }

        [HttpPost]
        [Authorize(Roles = "ROLE_ADMIN")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequest request)
        {
            var result = await _accountService.CreateAccount(request);
            _logger.LogInformation($"Account created successfully for email: {request.Email}");
            if (!result)
            {
                return BadRequest(new ApiResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Success = false,
                    Message = "Account creation failed",
                    Data = null
                });
            }
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Account created successfully",
                Data = result
            });
        }

        [HttpGet("{id}")]
        // [Authorize(Roles = "ROLE_ADMIN, ROLE_STAFF")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var account = await _accountService.GetById(id);
            if (account == null)
            {
                return NotFound(new ApiResponse
                {
                    Code = StatusCodes.Status404NotFound,
                    Success = false,
                    Message = "Account not found",
                    Data = null
                });
            }
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Account retrieved successfully",
                Data = account
            });
        }

        [HttpGet()]
        [Authorize(Roles = "ROLE_ADMIN, ROLE_STAFF")]
        [Description("API FOR ADMIN AND STAFF")]
        public async Task<IActionResult> GetWithParams([FromQuery] string? last_name, [FromQuery] string? first_name,
                                                        [FromQuery] EnumAccountStatus? status, [FromQuery] EnumRoleName? role,
                                                        [FromQuery] EnumGender? gender, [FromQuery] string? email,
                                                        [FromQuery] int page_current = 1, [FromQuery] int page_size = 10)
        {
            var result = await _accountService.GetWithParams(last_name, first_name, status, role, gender, email, page_current, page_size);
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Accounts retrieved successfully",
                Data = result
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(Guid id, [FromBody] UpdateAccountRequest request)
        {
            var result = await _accountService.UpdateAccount(request, id);
            if (!result)
            {
                return NotFound(new ApiResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Success = false,
                    Message = "Account not found",
                    Data = null
                });
            }
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Account updated successfully",
                Data = result
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ROLE_ADMIN")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var result = await _accountService.UpdateAccountStatus(id);
            if (!result)
            {
                return NotFound(new ApiResponse
                {
                    Code = StatusCodes.Status404NotFound,
                    Success = false,
                    Message = "Account not found",
                    Data = null
                });
            }
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Account deleted successfully",
                Data = result
            });
        }

        [HttpDelete("{idd}")]
        [Authorize(Roles = "ROLE_ADMIN")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _accountService.UpdateAccountStatus(id);
            if (!result)
            {
                return NotFound(new ApiResponse
                {
                    Code = StatusCodes.Status404NotFound,
                    Success = false,
                    Message = "Account not found",
                    Data = null
                });
            }
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Account deleted successfully",
                Data = result
            });
        }
    }
}

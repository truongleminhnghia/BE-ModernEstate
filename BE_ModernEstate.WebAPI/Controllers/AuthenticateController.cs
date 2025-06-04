using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.AuthenticateServices;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;


namespace BE_ModernEstate.WebAPI.Controllers
{
    [Route("api/v1/auths")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;

        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authenticateService.Login(request.Email, request.Password);
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Login successful",
                Data = result
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authenticateService.Register(request);
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Register successful",
                Data = null
            });
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            try
            {
                var result = await _authenticateService.VerifyEmailAsync(token);
                if (result)
                {
                    // Redirect đến trang thông báo thành công
                    return Redirect("https://modernestate.vercel.app/verify-success");
                }
                else
                {
                    // Redirect đến trang thông báo lỗi
                    return Redirect("https://modernestate.vercel.app/register");
                }
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("change-password/{id}")]
        [Authorize(Roles = "ROLE_CUSTOMER, ROLE_BROKER, ROLE_ADMIN, ROLE_STAFF, ROLE_PROPERTY_OWNER")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request, Guid id)
        {
            var result = await _authenticateService.ChangePassword(request.OldPassword, request.NewPassword, id);
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Change password successful",
                Data = null
            });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgetPasswordRequest request)
        {
            var result = await _authenticateService.ForgotPasswordAsync(request.Email);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassRequest request)
        {
            var result = await _authenticateService.ResetPasswordAsync(request.Token, request.NewPassword);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.PayosServices;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;
using Net.payOS.Types;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/checkout")]
    public class CheckoutController : ControllerBase
    {
        private readonly IPayosService _payosService;
        public CheckoutController(IPayosService payosService)
        {
            _payosService = payosService;
        }

        [HttpPost("create-payment-link/{id}")]
        public async Task<IActionResult> Checkout(Guid id)
        {
            var urlPayemt = await _payosService.CreatePaymentAsync(id);
            if (urlPayemt == null)
            {
                return BadRequest(new ApiResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Success = false,
                    Message = "Failed to create payment link.",
                    Data = null
                });
            }
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Payment link created successfully.",
                Data = urlPayemt
            });
        }

        [HttpGet("success")]
        public async Task<IActionResult> Success()
        {
            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Payment successful.",
                    Data = null
                }
            );
        }

        [HttpGet("failed")]
        public async Task<IActionResult> Failed()
        {
            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Thất bại",
                    Data = null
                }
            );
        }

        [HttpPost("verify-payment")]
        public async Task<IActionResult> VerifyPayment([FromBody] WebhookType type)
        {
            var resullt = await _payosService.VerifyPaymentAsync(type);
            if (resullt)
            {
                return Ok(new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Payment verified successfully.",
                    Data = null
                });
            }
            else
            {
                return BadRequest(new ApiResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Success = false,
                    Message = "Payment verification failed.",
                    Data = null
                });
            }
        }

    }
}
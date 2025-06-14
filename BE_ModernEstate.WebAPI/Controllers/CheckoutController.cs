
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.PayosServices;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;
using ModernEstate.DAL.Entites;
using Net.payOS;
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

        [HttpPost("/create-payment-link")]
        public async Task<IActionResult> Checkout([FromBody] PostPackageReuqest reuqest)
        {
            var urlPayemt = await _payosService.CreatePaymentAsync(reuqest);
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

        [HttpGet("/success")]
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

        [HttpGet("/failed")]
        public async Task<IActionResult> Failed()
        {
            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "thatas baij",
                    Data = null
                }
            );
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using ModernEstate.Common.Models.ApiResponse;
using Net.payOS;
using Net.payOS.Types;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/checkout")]
    public class CheckoutController : ControllerBase
    {
        private readonly PayOS _payOS;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CheckoutController(PayOS payOS, IHttpContextAccessor httpContextAccessor)
        {
            _payOS = payOS;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("/create-payment-link")]
        public async Task<IActionResult> Checkout()
        {
            try
            {
                int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
                ItemData item = new ItemData("Mì tôm hảo hảo ly", 1, 1000);
                List<ItemData> items = new List<ItemData> { item };

                // Get the current request's base URL
                var request = _httpContextAccessor.HttpContext.Request;
                var baseUrl = $"{request.Scheme}://{request.Host}";

                PaymentData paymentData = new PaymentData(
                    orderCode,
                    2000,
                    "Thanh toan don hang",
                    items,
                    $"https://localhost:8080/api/v1/checkout/failed",
                    $"https://localhost:8080/api/v1/checkout/success"
                );

                CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);

                return Ok(new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Payment link created successfully.",
                    Data = createPayment.checkoutUrl
                });
            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception);
                return Redirect("/");
            }
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
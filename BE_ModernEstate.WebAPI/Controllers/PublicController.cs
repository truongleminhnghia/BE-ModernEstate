﻿using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.AccountServices;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;
using Net.payOS;
using Net.payOS.Types;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [Route("api/v1/pulics")]
    [ApiController]
    public class PublicController : ControllerBase
    {
        private readonly PayOS _payOS;
        private readonly IAccountService _accountService;

        public PublicController(IAccountService accountService, PayOS payOS)
        {
            _accountService = accountService;
            _payOS = payOS;

        }

        [HttpPost]
        public async Task<ApiResponse> Register([FromBody] AccountRequest request)
        {
            var result = await _accountService.CreateAccount(request);

            if (result)
            {
                var response = new ApiResponse
                {
                    Code = StatusCodes.Status201Created,
                    Success = true,
                    Message = "Account created successfully.",
                    Data = request
                };
                return response;
            }
            else
            {
                var response = new ApiResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = "An error occurred while creating the account.",
                    Data = null
                };
                return response;
            }
        }

        [HttpPost("/createPaymentLink")]
        public async Task<IActionResult> CreatePaymentLink(CreatePaymentLinkRequest request)
        {
            try
            {
                int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
                ItemData item = new ItemData(request.productName, 1, request.price);
                List<ItemData> items = new List<ItemData>();
                items.Add(item);
                PaymentData paymentData = new PaymentData(orderCode, request.price, request.description, items, request.cancelUrl, request.returnUrl);

                CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);

                return Ok(
                    new ApiResponse
                    {
                        Code = 200,
                        Success = true,
                        Message = "",
                        Data = createPayment
                    }
                );
            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception);
                return Ok(new ApiResponse
                {
                    Code = 400,
                    Success = false,
                    Message = "",
                    Data = null
                });
            }
        }


    }
}

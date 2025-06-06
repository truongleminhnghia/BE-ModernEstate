﻿
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.AccountServices;
using ModernEstate.BLL.Services.BrokerServices;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.ApiResponse;


namespace BE_ModernEstate.WebAPI.Controllers
{
    [Route("api/v1/brokers")]
    [ApiController]
    public class BrokerController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IBrokerService _brokerService;

        public BrokerController(IAccountService accountService, IBrokerService brokerService)
        {
            _accountService = accountService;
            _brokerService = brokerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterBroker()
        {
            var userIdClaim = User.FindFirst("accountId");
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var accountId))
                throw new AppException(ErrorCode.UNAUTHORIZED);

            var result = await _brokerService.RegisterBroker(accountId);
            if (result.Success)
                return Ok(new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = result.Message
                }); else return BadRequest(new ApiResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Success = false,
                    Message = result.Message
                });
            
        }
    }
}

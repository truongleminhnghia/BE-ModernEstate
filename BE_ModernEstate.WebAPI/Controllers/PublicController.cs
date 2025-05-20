using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.AccountServices;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [Route("api/v1/pulics")]
    [ApiController]
    public class PublicController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public PublicController(IAccountService accountService)
        {
            _accountService = accountService;
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
    }
}

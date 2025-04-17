using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.AccountServices;
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

        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "ROLE_ADMIN, ROLE_STAFF, ROLE_MANAGER")]
        public async Task<ApiResponse> CreateAccount([FromBody] AccountRequest request)
        {
            try
            {
                _logger.LogInformation($"Creating account for email: {request.Email}");

                if (!User.Identity.IsAuthenticated)
                {
                    _logger.LogWarning("Unauthorized access attempt - User not authenticated");
                    return new ApiResponse
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        Success = false,
                        Message = "User not authenticated",
                        Data = null
                    };
                }

                var userRoles = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                                         .Select(c => c.Value)
                                         .ToList();

                _logger.LogInformation($"User roles: {string.Join(", ", userRoles)}");

                var result = await _accountService.CreateAccount(request, true);

                if (result)
                {
                    _logger.LogInformation($"Account created successfully for email: {request.Email}");
                    return new ApiResponse
                    {
                        Code = StatusCodes.Status201Created,
                        Success = true,
                        Message = "Account created successfully.",
                        Data = null
                    };
                }
                else
                {
                    _logger.LogError($"Failed to create account for email: {request.Email}");
                    return new ApiResponse
                    {
                        Code = StatusCodes.Status500InternalServerError,
                        Success = false,
                        Message = "An error occurred while creating the account.",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while creating account: {ex.Message}");
                return new ApiResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = "An unexpected error occurred.",
                    Data = null
                };
            }
        }

        // GET: api/accounts/{id}
        // [HttpGet("{id}")]
        // public async Task<ApiResponse> GetById(string id)
        // {
        //     var account = await _accountService.GetById(id);

        //     if (account == null)
        //     {
        //         var response = new ApiResponse
        //         {
        //             Code = StatusCodes.Status404NotFound,
        //             Success = false,
        //             Message = $"Account with ID {id} not found.",
        //             Data = null
        //         };
        //         return response;
        //     }

        //     var accountResponse = new AccountResponse
        //     {
        //         Id = account.Id,
        //         Email = account.Email,
        //         FirstName = account.FirstName,
        //         LastName = account.LastName,
        //         Phone = account.Phone,
        //         Address = account.Address,
        //         Role = account.Role
        //     };

        //     var successResponse = new ApiResponse
        //     {
        //         Code = StatusCodes.Status200OK,
        //         Success = true,
        //         Message = "Account retrieved successfully.",
        //         Data = accountResponse
        //     };

        //     return successResponse;
        // }
    }
}

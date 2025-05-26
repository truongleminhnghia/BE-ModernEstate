using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.TransactionServices;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [Route("api/v1/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(
            ITransactionService transactionService,
            ILogger<TransactionController> logger
        )
        {
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetWithParams(
            [FromQuery] Guid? accountId,
            [FromQuery] EnumTypeTransaction? typeTransaction,
            [FromQuery] EnumStatusPayment? status,
            [FromQuery] EnumPaymentMethod? paymentMethod,
            [FromQuery(Name = "page_current")] int pageCurrent = 1,
            [FromQuery(Name = "page_size")] int pageSize = 10
        )
        {
            var page = await _transactionService.GetWithParamsAsync(
                accountId,
                typeTransaction,
                status,
                paymentMethod,
                pageCurrent,
                pageSize
            );

            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Transactions retrieved successfully",
                    Data = page,
                }
            );
        }

        [HttpPost("bycashs")]
        //[Authorize(Roles = "ROLE_MANAGER, ROLE_STAFF, ROLE_CUSTOMER")]
        public async Task<IActionResult> PayByCash([FromBody] CashPaymentRequest dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Success = false,
                        Message = "Invalid request data",
                        Data = ModelState,
                    }
                );

            try
            {
                var result = await _transactionService.PayByCashAsync(dto);

                return CreatedAtAction(
                    nameof(PayByCash),
                    new { id = result.TransactionId },
                    new ApiResponse
                    {
                        Code = StatusCodes.Status201Created,
                        Success = true,
                        Message = "Cash payment processed successfully",
                        Data = result,
                    }
                );
            }
            catch (KeyNotFoundException knf)
            {
                _logger.LogWarning($"Cash payment failed: {knf.Message}");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = knf.Message,
                        Data = null,
                    }
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing cash payment");
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ApiResponse
                    {
                        Code = StatusCodes.Status500InternalServerError,
                        Success = false,
                        Message = "Internal server error",
                        Data = null,
                    }
                );
            }
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "ROLE_MANAGER, ROLE_STAFF, ROLE_CUSTOMER")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dto = await _transactionService.GetByIdAsync(id);
            if (dto == null)
            {
                _logger.LogWarning($"Transaction not found with id {id}");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Transaction not found",
                        Data = null,
                    }
                );
            }

            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Transaction retrieved successfully",
                    Data = dto,
                }
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.InvetorServices;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/inventors")]
    public class InventorController : ControllerBase
    {
        private readonly IInvetorService _invetorService;
        private readonly ILogger<AccountController> _logger;

        public InventorController(IInvetorService invetorService, ILogger<AccountController> logger)
        {
            _invetorService = invetorService;
            _logger = logger;
        }

        [HttpPost]
        // [Authorize(Roles = "ROLE_ADMIN")]
        public async Task<IActionResult> CreateAccount([FromBody] InvetorRequest request)
        {
            var result = await _invetorService.CreateInventor(request);
            _logger.LogInformation($"Inventor created successfully for email: {request.Name}");
            if (!result)
            {
                return BadRequest(new ApiResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Success = false,
                    Message = "Inventor creation failed",
                    Data = null
                });
            }
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Inventor created successfully",
                Data = result
            });
        }

        [HttpGet("{id}")]
        // [Authorize(Roles = "ROLE_ADMIN, ROLE_STAFF")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var account = await _invetorService.GetById(id);
            if (account == null)
            {
                return NotFound(new ApiResponse
                {
                    Code = StatusCodes.Status404NotFound,
                    Success = false,
                    Message = "Inventor not found",
                    Data = null
                });
            }
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Inventor retrieved successfully",
                Data = account
            });
        }

        [HttpGet()]
        // [Authorize(Roles = "ROLE_ADMIN, ROLE_STAFF")]
        // [Description("API FOR ADMIN AND STAFF")]
        public async Task<IActionResult> GetWithParams([FromQuery] string? name, [FromQuery] string? companyName,
                                                        [FromQuery] EnumInvetorType? invetorType, [FromQuery] string? email,
                                                        [FromQuery] int page_current = 1, [FromQuery] int page_size = 10)
        {
            var result = await _invetorService.GetWithParams(name, companyName, invetorType, email, page_current, page_size);
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Accounts retrieved successfully",
                Data = result
            });
        }
    }
}
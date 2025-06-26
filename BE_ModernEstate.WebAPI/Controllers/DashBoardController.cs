using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.DashBoardServices;
using ModernEstate.Common.Models.ApiResponse;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/dashboards")]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardService _service;

        public DashBoardController(IDashBoardService service)
        {
            _service = service;
        }

        [HttpGet("accounts")]
        public async Task<IActionResult> Accounts()
        {
            var account = await _service.GetAccountDashBoardAsync();
            if (account == null)
                return BadRequest(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Success = false,
                        Message = "Thông tin account đang trống",
                        Data = null
                    }
                );
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Lấy dữ liệu thành công",
                Data = account
            });
        }

        [HttpGet("posts")]
        public async Task<IActionResult> Posts()
        {
            var post = await _service.Post();
            if (post == null)
            {
                return BadRequest(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Success = false,
                        Message = "Thông tin post đang trống",
                        Data = null
                    }
                );
            }
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Lấy dữ liệu thành công",
                Data = post
            });
        }

        [HttpGet("revenue")]
        public async Task<IActionResult> GetRevenue()
        {
            double total = await _service.GetTotalAmountAsync(); // all filters default to null
            return Ok(new ApiResponse
            { 
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Lấy dữ liệu thành công",
                Data = new { TotalRevenue = total }
            });
        }
    }
}
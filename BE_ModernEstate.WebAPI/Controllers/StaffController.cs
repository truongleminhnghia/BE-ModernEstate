using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ModernEstate.BLL.Services.PostServices;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/staffs")]
    public class StaffController : ControllerBase
    {
        private readonly IPostService _postService;

        public StaffController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPut("approve-post/{id}")]
        [Authorize(Roles = "ROLE_STAFF")]
        public async Task<IActionResult> ConfirmPost(Guid id, [FromBody] ConfirmPostRequest request)
        {
            var result = await _postService.ConfirmPost(id, request);
            if (result)
            {
                return Ok(new ApiResponse
                {
                    Code = 200,
                    Message = "Thành công",
                    Success = true,
                    Data = null
                });
            }
            return BadRequest(new ApiResponse
            {
                Code = 400,
                Message = "Thất bại",
                Success = false,
                Data = null
            });
        }

        [HttpGet("post-wait-approve")]
        [Authorize(Roles = "ROLE_ADMIN, ROLE_STAFF, ROLE_CUSTOMER, ROLE_BROKER")]
        public async Task<IActionResult> GetPost([FromQuery] int page_current = 1, [FromQuery] int page_size = 10)
        {
            var result = await _postService.GetPostConfirm(page_current, page_size);
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Lấy dữ liệu thành công",
                Data = result
            });
        }
    }
}
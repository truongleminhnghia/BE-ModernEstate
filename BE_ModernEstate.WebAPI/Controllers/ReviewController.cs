using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.ReviewServices;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] ReviewRequest request)
        {
            var result = await _reviewService.CreateReview(request);
            if (result)
            {
                return Ok(new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Đánh giá thành công",
                    Success = true,
                    Data = null
                });
            }
            return BadRequest(new ApiResponse
            {
                Code = StatusCodes.Status400BadRequest,
                Message = "Đánh giá thất bại",
                Success = false,
                Data = null
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _reviewService.GetById(id);
            if (result != null)
            {
                return Ok(new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Lấy dữ liệu thành công",
                    Success = true,
                    Data = result
                });
            }
            return BadRequest(new ApiResponse
            {
                Code = StatusCodes.Status400BadRequest,
                Message = "Lấy dữ liệu thất bại",
                Success = false,
                Data = null
            });
        }

        [HttpGet()]
        public async Task<IActionResult> GetReviews([FromQuery] Guid? accountId, [FromQuery] float? fromRating, [FromQuery] float? toRating,
                                                    [FromQuery] string? comment, [FromQuery] int pageCurrent = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _reviewService.GetReviews(accountId, fromRating, toRating, comment, pageCurrent, pageSize);
            if (result != null)
            {
                return Ok(new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Lấy dữ liệu thành công",
                    Success = true,
                    Data = result
                });
            }
            return BadRequest(new ApiResponse
            {
                Code = StatusCodes.Status400BadRequest,
                Message = "Lấy dữ liệu thất bại",
                Success = false,
                Data = null
            });
        }
    }
}
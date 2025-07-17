
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.PostServices;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ILogger<PostController> _logger;

        public PostController(IPostService postService, ILogger<PostController> logger)
        {
            _postService = postService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostRequest request)
        {
            var result = await _postService.CreatePost(request);
            if (result == null)
            {
                return BadRequest(new ApiResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Success = false,
                    Message = "Post creation failed",
                    Data = null
                });
            }
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Post creation successfully",
                Data = result
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _postService.GetById(id);
            if (result == null)
            {
                return NotFound(new ApiResponse
                {
                    Code = StatusCodes.Status404NotFound,
                    Success = false,
                    Message = "Post not found",
                    Data = null
                });
            }
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Post retrieved successfully",
                Data = result
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts([FromQuery] EnumDemand? demand, [FromQuery] EnumSourceStatus? srcStatus, [FromQuery] Guid? postBy,
                                                 [FromQuery] EnumStatus? status, [FromQuery] Guid? approveBy, [FromQuery] EnumPriorityStatus? priority,
                                                [FromQuery] int page_current = 1, [FromQuery] int page_size = 10)
        {
            var result = await _postService.GetPosts(demand, srcStatus, postBy, status, approveBy, priority, page_current, page_size);
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Accounts retrieved successfully",
                Data = result
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost([FromBody] UpdatePostRequest request, Guid id)
        {
            return Ok();
        }
    }
}
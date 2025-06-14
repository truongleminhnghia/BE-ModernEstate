
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
            if (!result)
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
                Message = "Account created successfully",
                Data = "result"
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
        public async Task<IActionResult> GetPosts([FromQuery] string? title, [FromQuery] EnumStatePost? state,
                                                    [FromQuery] EnumSourceStatus? srcStatus,
                                                    [FromQuery] int page_current = 1, [FromQuery] int page_size = 10)
        {
            var result = await _postService.GetPosts(title, state, srcStatus, page_current, page_size);
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
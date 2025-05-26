
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.PostServices;
using ModernEstate.Common.Models.Requests;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostRequest request)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost([FromBody] UpdatePostRequest request, Guid id)
        {
            return Ok();
        }
    }
}
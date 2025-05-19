using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.PostPackageServices;
using ModernEstate.Common.Models.Requests;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/postpackages")]
    public class PostPackageController : ControllerBase
    {
        private readonly IPostPackageService _postPackageService;

        public PostPackageController(IPostPackageService postPackageService)
        {
            _postPackageService = postPackageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _postPackageService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await _postPackageService.GetByIdAsync(id);
            return entity == null ? NotFound() : Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostPackageRequest request)
        {
            var entity = await _postPackageService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PostPackageRequest request)
        {
            var updated = await _postPackageService.UpdateAsync(id, request);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _postPackageService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}

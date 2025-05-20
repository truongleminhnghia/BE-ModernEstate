
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.ProjectServices;
using ModernEstate.Common.Models.Requests;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateProject([FromBody] ProjectRequest request)
        {
            return Ok();
        }

        [HttpGet()]
        public async Task<IActionResult> GetProject()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok();
        }

    }
}
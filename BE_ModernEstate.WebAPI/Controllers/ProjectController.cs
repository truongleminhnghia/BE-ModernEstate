
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.ProjectServices;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateProject([FromBody] ProjectRequest request)
        {
            var result = await _projectService.SaveProject(request);
            if (!result)
            {
                return BadRequest(new ApiResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Success = false,
                    Message = "Account creation failed",
                    Data = null
                });
            }
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Account created successfully",
                Data = result
            });
        }

        [HttpGet()]
        public async Task<IActionResult> GetProject([FromQuery] EnumProjectType? projectType, [FromQuery] string? title,
                                                    [FromQuery] float? minArea, [FromQuery] float? maxArea, [FromQuery] EnumProjectStatus? projectStatus,
                                                    [FromQuery] string? invetorName, [FromQuery] int page_current = 1, [FromQuery] int page_size = 10)
        {
            var result = await _projectService.GetProjects(projectType, title, minArea, maxArea, projectStatus, invetorName, page_current, page_size);
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Accounts retrieved successfully",
                Data = result
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var project = await _projectService.GetById(id);
            if (project == null)
            {
                return NotFound(new ApiResponse
                {
                    Code = StatusCodes.Status404NotFound,
                    Success = false,
                    Message = "Account not found",
                    Data = null
                });
            }
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Account retrieved successfully",
                Data = project
            });
        }

    }
}
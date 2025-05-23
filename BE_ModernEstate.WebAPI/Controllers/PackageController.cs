using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.PackageServices;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/packages")]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _svc;
        private readonly ILogger<PackageController> _logger;

        public PackageController(IPackageService svc, ILogger<PackageController> logger)
        {
            _svc = svc;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "ROLE_MANAGER, ROLE_STAFF")]
        public async Task<IActionResult> GetWithParams(
            [FromQuery] EnumTypePackage? typePackage,
            [FromQuery(Name = "page_current")] int pageCurrent = 1,
            [FromQuery(Name = "page_size")] int pageSize = 10
        )
        {
            var page = await _svc.GetWithParamsAsync(typePackage, pageCurrent, pageSize);
            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Packages retrieved successfully",
                    Data = page,
                }
            );
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ROLE_MANAGER, ROLE_STAFF")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dto = await _svc.GetByIdAsync(id);
            if (dto == null)
            {
                _logger.LogWarning($"Package not found with id {id}");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Package not found",
                        Data = null,
                    }
                );
            }

            return Ok(
                new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Package retrieved successfully",
                    Data = dto,
                }
            );
        }

        [HttpPost]
        [Authorize(Roles = "ROLE_MANAGER, ROLE_STAFF")]
        public async Task<IActionResult> Create([FromBody] PackageRequest request)
        {
            var created = await _svc.CreateAsync(request);
            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                new ApiResponse
                {
                    Code = StatusCodes.Status201Created,
                    Success = true,
                    Message = "Package created successfully",
                    Data = created,
                }
            );
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ROLE_MANAGER, ROLE_STAFF")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PackageRequest request)
        {
            if (!await _svc.UpdateAsync(id, request))
            {
                _logger.LogWarning($"Failed to update package with id {id} - not found");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Package not found",
                        Data = null,
                    }
                );
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ROLE_MANAGER, ROLE_STAFF")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _svc.DeleteAsync(id))
            {
                _logger.LogWarning($"Failed to delete package with id {id} - not found");
                return NotFound(
                    new ApiResponse
                    {
                        Code = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Package not found",
                        Data = null,
                    }
                );
            }
            return NoContent();
        }
    }
}

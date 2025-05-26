using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.NewServices;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [Route("api/v1/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewService _service;
        private readonly IMapper _mapper;

        public NewsController(INewService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewsRequest news)
        {

            var created = await _service.CreateAsync(news);
            if (created.Success)
                return Ok(new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = created.Message
                });
            else return BadRequest(new ApiResponse
            {
                Code = StatusCodes.Status400BadRequest,
                Success = false,
                Message = created.Message
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] NewsRequest news)
        {
            var result = await _service.UpdateAsync(id, news);
            if (result.Success)
                return Ok(new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = result.Message
                });
            else
                return BadRequest(new ApiResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Success = false,
                    Message = result.Message
                });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                return Ok(new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = "Successfully",
                    Data = result
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponse
                {
                    Code = StatusCodes.Status404NotFound,
                    Success = false,
                    Message = "News not found."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Success = false,
                    Message = $"Error: {ex.Message}"
                });
            }

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> ToggleDelete(Guid id)
        {
            try
            {
                var result = await _service.ToggleStatusAsync(id);

                var message = result == EnumStatusNew.ARCHIVED
                    ? "News has been archived successfully"
                    : "News has been published successfully";

                return Ok(new ApiResponse
                {
                    Code = StatusCodes.Status200OK,
                    Success = true,
                    Message = message
                });
            }
            
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredNews(
    [FromQuery] string? title,
    [FromQuery] EnumStatusNew? status,
    [FromQuery] EnumCategoryName? categoryName,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetWithParamsAsync(title, status, categoryName, page, pageSize);
            return Ok(new ApiResponse
            {
                Code = 200,
                Success = true,
                Message = "Filtered news retrieved successfully",
                Data = result
            });
        }




    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.Services.NewServices;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;

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
            return Ok(new ApiResponse
            {
                Code = StatusCodes.Status200OK,
                Success = true,
                Message = "Create news successful",
                Data = null
            });
        }
    }
}

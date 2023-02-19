using FireAndForgetHandler.Model.Dto;
using FireAndForgetHandler.Services;
using Microsoft.AspNetCore.Mvc;

namespace FireAndForgetHandler.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly StatusService _statusService;

        public StatusController(StatusService statusService) 
        { 
            _statusService = statusService;
        }

        // api/v1/status/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> StatusAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            TaskStatusInfoResponse? statusInfo = await _statusService
                .GetStatusById(id);

            return statusInfo == null ? NotFound() : Ok(statusInfo);

        }
    }
}

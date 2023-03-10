using FireAndForgetHandler.Extensions;
using FireAndForgetHandler.Handler;
using FireAndForgetHandler.Model;
using FireAndForgetHandler.Model.Dto;
using FireAndForgetHandler.Services;
using Microsoft.AspNetCore.Mvc;

namespace FireAndForgetHandler.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IFireAndForgetTaskHandler _fireAndForgetTaskHandler;
        private readonly StatusService _statusService;

        public EmailController(IFireAndForgetTaskHandler fireAndForgetTaskHandler, 
            StatusService statusService)
        {
            _fireAndForgetTaskHandler = fireAndForgetTaskHandler;
            _statusService = statusService;
        }

        // api/v1/Email
        [HttpPost]
        [ProducesResponseType(typeof(TaskStatusInfoResponse), 201)]
        public async Task<IActionResult> SendEmailAsync([FromBody] EmailRequest email)
        {
            TaskStatusInfo taskStatusInfo = await _statusService
                .CreateTaskStatus();

            _fireAndForgetTaskHandler
                .ExecuteAsync<EmailService>(option => 
                    option.SendEmailAsync(email), taskStatusInfo);

            return Created(string.Format("{0}://{1}/api/v1/Status/{2}",
                Request.Scheme,
                Request.Host,
                taskStatusInfo.Id), 
                taskStatusInfo.Response());

        }
    }
}

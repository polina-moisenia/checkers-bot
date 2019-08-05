using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckersBot.Controllers
{
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet("readiness")]
        public ContentResult GetReadiness(CancellationToken cancellationToken)
        {
            var response = Content("The app is up", "text/plain");
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
    }
}

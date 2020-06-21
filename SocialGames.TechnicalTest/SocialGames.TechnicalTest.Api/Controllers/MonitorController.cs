using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace SocialGames.TechnicalTest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonitorController : ControllerBase
    {
        public MonitorController()
        {

        }

        [HttpGet("/")]
        public IActionResult Heartbeat()
        {
            return Ok();
        }

        [HttpGet]
        [Route("ping")]
        public IActionResult Ping()
        {
            return Ok("pong");
        }
    }
}


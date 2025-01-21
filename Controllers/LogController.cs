using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Channels;

namespace NewWebApplication2.Controllers
{
    [Route("api/logs")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly Channel<string> _logChannel;

        public LogController(Channel<string> logChannel)
        {
            _logChannel = logChannel;
        }

        [HttpPost]
        public IActionResult WriteLog([FromBody] string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return BadRequest("Log message cannot be empty.");
            }

            // Dodajemy wiadomość do kolejki
            _logChannel.Writer.TryWrite(message);

            return Ok("Message logged successfully.");
        }
    }
}

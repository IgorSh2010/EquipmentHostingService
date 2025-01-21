using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NewWebApplication2.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            string html = "<h2>Welcome to the Web API Hosting Service</h2>" +
                          "<p>Explore the API using <a href=\"https://webapphostingservice.azurewebsites.net/swagger\">Swagger</a>.</p>";
            return Content(html, "text/html");
        }
    }
}

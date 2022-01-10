using Application;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("")]
    public class RootController : Controller
    {
        [HttpGet]
        public ContentResult GetFrontendUrl([FromServices] ApplicationSettings settings)
        { 
            return base.Content($"<a href={settings.FrontendUrl}>{settings.FrontendUrl}</a>", "text/html");
        }
    }
}
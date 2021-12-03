using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/test")]

    public class TestController : BaseController
    {
        [HttpGet]
        [Authorize]
        public ActionResult RegisterUser()
        {
            return Ok("hello");
        }
    }
}
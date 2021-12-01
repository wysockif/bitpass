using System.Threading.Tasks;
using Application.Commands;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("accounts")]
    public class AccountController : BaseController
    {

        [HttpPost("register")]
        public async Task<AuthViewModel> RegisterAppUser([FromBody] RegisterAppUserCommand command)
        {
            command.UserAgent = Request.Headers["User-Agent"];
            command.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            return await Mediator.Send(command);
        }
    }
}
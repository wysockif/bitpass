using System.Threading.Tasks;
using Application.Commands;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/accounts")]
    public class AccountController : BaseController
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<AuthViewModel> RegisterUser([FromBody] RegisterUserCommand command)
        {
            command.UserAgent = Request.Headers["User-Agent"];
            command.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            return await Mediator.Send(command);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<AuthViewModel> LoginUser([FromBody] LoginUserCommand command)
        {
            command.UserAgent = Request.Headers["User-Agent"];
            command.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            return await Mediator.Send(command);
        }
    }
}
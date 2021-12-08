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
        public async Task<ActionResult<AuthViewModel>> LoginUser([FromBody] LoginUserCommand command)
        {
            command.UserAgent = Request.Headers["User-Agent"];
            command.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("refresh-access-token")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthViewModel>> RefreshAccessToken([FromBody] RefreshAccessTokenCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("verify-email-address")]
        [AllowAnonymous]
        public async Task<ActionResult> VerifyEmailAddress([FromBody] VerifyEmailAddressCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
using System.Threading.Tasks;
using Application.Commands;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/accounts")]
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserCommand command)
        {
            command.UserAgent = Request.Headers["User-Agent"];
            command.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthViewModel>> LoginUser([FromBody] LoginUserCommand command)
        {
            command.UserAgent = Request.Headers["User-Agent"];
            command.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("refresh-access-token")]
        public async Task<ActionResult<AuthViewModel>> RefreshAccessToken([FromBody] RefreshAccessTokenCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("verify-email-address")]
        public async Task<ActionResult> VerifyEmailAddress([FromBody] VerifyEmailAddressCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost("request-password-reset")]
        public async Task<ActionResult> RequestPasswordReset([FromBody] RequestResetPasswordCommand command)
        {
            command.UserAgent = Request.Headers["User-Agent"];
            command.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            command.UserAgent = Request.Headers["User-Agent"];
            command.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            command.UserAgent = Request.Headers["User-Agent"];
            command.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
using System;
using System.Threading.Tasks;
using Application.Commands;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UAParser;

namespace Api.Controllers
{
    [Route("api/accounts")]
    public class AccountController : BaseController
    {
        // public AccountController(IMediator mediator) : base(mediator)
        // {
        // }

        // [HttpGet]

        // public string Login()

        // {

        //     // Request.

        //     

        //     // return ReHttpContext.Connection.RemoteIpAddress.ToString();

        //     // Response.Headers["Server"] = "Cowboy";

        //

        //     var k = Request.Headers["User-Agent"].ToString();

        //     

        //     var t = k.IndexOf('(');

        //     var a = k.IndexOf(';');

        //     var z = k.IndexOf(')');

        //     Console.WriteLine(k.Substring(z+1, ));

        //     

        //     return k.Substring(t+1, a-t-1);

        //     

        // }


        [HttpPost("register")]
        public async Task<AuthViewModel> RegisterAppUser()
        {
            var command = new RegisterAppUserCommand
            {
                UserAgent = Request.Headers["User-Agent"]
            };
            return await Mediator.Send(command);
        }
    }
}
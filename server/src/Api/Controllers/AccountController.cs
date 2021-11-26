using System;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
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
    }
}
using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Controllers
{
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected string? GetIpAddress(HttpContext httpContext)
        {
            var ip = httpContext.Request.Headers["x-forwarded-for"].Count != default
                ? httpContext.Request.Headers["x-forwarded-for"].ToString().Split(',')[
                    httpContext.Request.Headers["x-forwarded-for"].ToString().Split(',').Length - 1]
                : httpContext.Connection.RemoteIpAddress?.ToString();
            Console.WriteLine(ip);
            return ip;
        }
    }
}
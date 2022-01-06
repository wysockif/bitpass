using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Api.Middlewares
{
    public class ResponseHeaderModifyingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Response.Headers["Server"] = "nginx";
            await next.Invoke(context);
        }
    }
}
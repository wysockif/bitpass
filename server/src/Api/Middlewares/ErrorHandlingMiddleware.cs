using System.Security.Authentication;
using System.Threading.Tasks;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Api.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (BadRequestException badRequestException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(badRequestException.Message);
            }
            catch (AuthenticationException authenticationException)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync(authenticationException.Message);
            }
            // catch (Exception e)
            // {
            //     context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //     await context.Response.WriteAsync("Something went wrong");
            // }
        }
    }
}
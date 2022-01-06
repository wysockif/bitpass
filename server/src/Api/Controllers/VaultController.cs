using System.Threading.Tasks;
using Application.Commands;
using Application.Queries;
using Application.Utils.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/vault")]
    [Authorize]
    [ApiController]
    public class VaultController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> GetVault()
        {
            var query = new GetVaultQuery
            {
                UserId = AuthorizationService.RequireUserId(HttpContext.User),
                UserAgent = Request.Headers["User-Agent"],
                IpAddress = GetIpAddress(HttpContext)
            };
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<ActionResult> AddCipherLogin([FromBody] AddCipherLoginCommand command)
        {
            command.UserId = AuthorizationService.RequireUserId(HttpContext.User);
            command.UserAgent = Request.Headers["User-Agent"];
            command.IpAddress = GetIpAddress(HttpContext);
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;

namespace Application.Utils.Authorization
{
    public static class AuthorizationService
    {
        public static long RequireUserId(ClaimsPrincipal user)
        {
            var idClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (idClaim == default)
            {
                throw new AuthenticationException("You are not authorized");
            }

            return long.Parse(idClaim.Value);
        }
    }
}
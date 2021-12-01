using System.Threading.Tasks;
using Domain.Model;

namespace Domain.Services
{
    public interface IAccountService
    {
        Task<User> RegisterAsync(string email, string username, string password, string masterPassword);
        Task<AuthToken> LoginAsync(string identifier, string password, string? ipAddress, string? userAgent);
    }

    public record AuthToken(string AccessToken);
}
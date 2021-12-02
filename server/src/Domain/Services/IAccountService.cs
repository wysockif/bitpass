using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IAccountService
    {
        Task<Auth> RegisterAsync(string email, string username, string password, string masterPassword,
            string? ipAddress, string? userAgent);

        Task<Auth> LoginAsync(string identifier, string password, string? ipAddress, string? userAgent);
    }

    public record Auth(string AccessToken);
}
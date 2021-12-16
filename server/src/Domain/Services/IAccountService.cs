using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IAccountService
    {
        Task RegisterAsync(string email, string username, string password, string encryptionKeyHash,
            string? ipAddress, string? userAgent);

        Task<Auth> LoginAsync(string identifier, string password, string? ipAddress, string? userAgent);
    }

    public record Auth(string AccessToken, string RefreshToken, string UniversalToken);
}
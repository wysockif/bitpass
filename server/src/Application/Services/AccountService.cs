using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Application.InfrastructureInterfaces;
using Domain.Model;
using Domain.Services;
using UAParser;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> RegisterAsync(string email, string username, string password, string masterPassword)
        {
            var user = await _unitOfWork.AppUserRepository.GetByEmailOrUsernameAsync(email, username);

            if (user != default)
            {
                var propertyName = user.Email == email.ToLower()
                    ? nameof(email)
                    : nameof(username);
                throw new Exception($"User with given {propertyName} already exists");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password + "PEPPER", 16);
            var masterPasswordHash = BCrypt.Net.BCrypt.HashPassword(password + "PEPPER2", 16);
            var registeredUser = User.Register(username, email, passwordHash, masterPasswordHash);
            await _unitOfWork.AppUserRepository.AddAsync(registeredUser);
            await _unitOfWork.SaveChangesAsync();
            return registeredUser;
        }

        public async Task<AuthToken> LoginAsync(string identifier, string password, string? ipAddress,
            string? userAgent)
        {
            var user = await _unitOfWork.AppUserRepository.GetByEmailOrUsernameAsync(identifier.ToLower(),
                identifier.ToLower());

            if (user == default)
            {
                throw new AuthenticationException("Invalid credentials");
            }

            var (osName, browserName) = GetDeviceInfo(userAgent);

            var isPasswordVerified = BCrypt.Net.BCrypt.Verify(password + "PEPPER", user.PasswordHash);

            if (!isPasswordVerified)
            {
                user.AddAccountActivity(ActivityType.FailedLogin, ipAddress, osName, browserName);
                await _unitOfWork.SaveChangesAsync();
                throw new AuthenticationException("Invalid credentials");
            }

            user.AddAccountActivity(ActivityType.SuccessfulLogin, ipAddress, osName, browserName);
            await _unitOfWork.SaveChangesAsync();

            return new AuthToken("asdasfa");
        }

        private static (string? osName, string? browserName) GetDeviceInfo(string? userAgent)
        {
            var clientInfo = string.IsNullOrEmpty(userAgent) ? null : Parser.GetDefault().Parse(userAgent);
            var osName = clientInfo == null ? null : clientInfo.OS.Family + " " + clientInfo.OS.Major;
            var browserName = clientInfo == null ? null : clientInfo.UA.Family + " " + clientInfo.UA.Major;
            return (osName, browserName);
        }
    }
}
using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Application.InfrastructureInterfaces;
using Application.Settings;
using Application.Utils.Security;
using Domain.Model;
using Domain.Services;
using UAParser;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationSettings _settings;
        private readonly ISecurityTokenService _securityTokenService;

        public AccountService(IUnitOfWork unitOfWork, ApplicationSettings settings,
            ISecurityTokenService securityTokenService)
        {
            _unitOfWork = unitOfWork;
            _settings = settings;
            _securityTokenService = securityTokenService;
        }

        public async Task<Auth> RegisterAsync(string email, string username, string password, string masterPassword,
            string? ipAddress,
            string? userAgent)
        {
            await CheckIfUserAlreadyExists(email, username);

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password + _settings.PasswordPepper, 15);
            var masterPasswordHash =
                BCrypt.Net.BCrypt.HashPassword(masterPassword + _settings.MasterPasswordPepper, 14);

            var registeredUser = User.Register(username.ToLower(), email.ToLower(), passwordHash, masterPasswordHash);
            var (osName, browserName) = GetDeviceInfo(userAgent);
            await _unitOfWork.UserRepository.AddAsync(registeredUser);
            await _unitOfWork.SaveChangesAsync();

            var user = await _unitOfWork.UserRepository.GetByIdAsync(registeredUser.Id);
            user!.AddAccountActivity(ActivityType.SuccessfulRegistration, ipAddress, osName, browserName);
            await _unitOfWork.SaveChangesAsync();
            return new Auth(_securityTokenService.GenerateAccessTokenForUser(user.Id, user.Username));
        }

        public async Task<Auth> LoginAsync(string identifier, string password, string? ipAddress,
            string? userAgent)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailOrUsernameAsync(identifier.ToLower(),
                identifier.ToLower());
            if (user == default)
            {
                throw new AuthenticationException("Invalid credentials");
            }

            await CheckInvalidLoginAttemptsNumber(user.Id);
            var (osName, browserName) = GetDeviceInfo(userAgent);

            var isPasswordVerified = BCrypt.Net.BCrypt.Verify(password + _settings.PasswordPepper, user.PasswordHash);
            if (!isPasswordVerified)
            {
                user.AddAccountActivity(ActivityType.FailedLogin, ipAddress, osName, browserName);
                await _unitOfWork.SaveChangesAsync();
                throw new AuthenticationException("Invalid credentials");
            }

            user.AddAccountActivity(ActivityType.SuccessfulLogin, ipAddress, osName, browserName);
            await _unitOfWork.SaveChangesAsync();
            return new Auth(_securityTokenService.GenerateAccessTokenForUser(user.Id, user.Username));
        }

        private async Task CheckIfUserAlreadyExists(string email, string username)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailOrUsernameAsync(email, username);

            if (user != default)
            {
                var propertyName = user.Email == email.ToLower()
                    ? nameof(email)
                    : nameof(username);
                throw new Exception($"User with given {propertyName} already exists");
            }
        }

        private async Task CheckInvalidLoginAttemptsNumber(long userId)
        {
            if (await _unitOfWork.UserRepository.GetFailedLoginActivitiesCountInLastHourByUserId(userId) > 3)
            {
                throw new AuthenticationException("Too many invalid login attempts. Try again later");
            }
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
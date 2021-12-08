using System.Threading.Tasks;
using Application.Exceptions;
using Application.InfrastructureInterfaces;
using Application.Utils.Security;
using Domain.Model;
using Domain.Services;
using UAParser;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly ISecurityTokenService _securityTokenService;
        private readonly ApplicationSettings _settings;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork, ApplicationSettings settings,
            ISecurityTokenService securityTokenService)
        {
            _unitOfWork = unitOfWork;
            _settings = settings;
            _securityTokenService = securityTokenService;
        }

        public async Task<Auth> RegisterAsync(string email, string username, string password, string masterPassword,
            string? ipAddress, string? userAgent)
        {
            await CheckIfUserAlreadyExists(email, username);

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password + _settings.PasswordPepper, 14);
            var masterPasswordHash =
                BCrypt.Net.BCrypt.HashPassword(masterPassword + _settings.MasterPasswordPepper, 12);
            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            var registeredUser = User.Register(username.ToLower(), email.ToLower(), passwordHash, masterPasswordHash);
            await _unitOfWork.UserRepository.AddAsync(registeredUser);
            await _unitOfWork.SaveChangesAsync();

            var (osName, browserName) = GetDeviceInfo(userAgent);

            var accessToken =
                _securityTokenService.GenerateAccessTokenForUser(registeredUser.Id, registeredUser.Username);
            var refreshToken =
                _securityTokenService.GenerateRefreshTokenForUser(registeredUser.Id, registeredUser.Username);

            registeredUser.AddSession(refreshToken.TokenGuid, refreshToken.ExpirationTimestamp, ipAddress, osName,
                browserName);
            registeredUser.AddAccountActivity(ActivityType.SuccessfulRegistration, ipAddress, osName, browserName);

            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();

            return new Auth(accessToken.Token, refreshToken.Token);
        }

        public async Task<Auth> LoginAsync(string identifier, string password, string? ipAddress,
            string? userAgent)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailOrUsernameAsync(identifier.ToLower(),
                identifier.ToLower());
            if (user == default)
            {
                await Task.Delay(ApplicationConstants.InvalidLoginDelayInMilliseconds);
                throw new BadRequestException("Invalid credentials");
            }

            await CheckInvalidLoginAttemptsNumber(user.Id);
            var (osName, browserName) = GetDeviceInfo(userAgent);
            await VerifyPassword(password, ipAddress, user, osName, browserName);
            user.AddAccountActivity(ActivityType.SuccessfulLogin, ipAddress, osName, browserName);

            var accessToken = _securityTokenService.GenerateAccessTokenForUser(user.Id, user.Username);
            var refreshToken = _securityTokenService.GenerateRefreshTokenForUser(user.Id, user.Username);

            user.AddSession(refreshToken.TokenGuid, refreshToken.ExpirationTimestamp, ipAddress, osName, browserName);
            await _unitOfWork.SaveChangesAsync();

            return new Auth(accessToken.Token, refreshToken.Token);
        }

        private async Task VerifyPassword(string password, string? ipAddress, User user, string? osName,
            string? browserName)
        {
            var isPasswordVerified = BCrypt.Net.BCrypt.Verify(password + _settings.PasswordPepper, user.PasswordHash);
            if (!isPasswordVerified)
            {
                user.AddAccountActivity(ActivityType.FailedLogin, ipAddress, osName, browserName);
                await _unitOfWork.SaveChangesAsync();
                throw new BadRequestException("Invalid credentials");
            }
        }

        private async Task CheckIfUserAlreadyExists(string email, string username)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailOrUsernameAsync(email, username);

            if (user != default)
            {
                var propertyName = user.Email == email.ToLower()
                    ? nameof(email)
                    : nameof(username);
                throw new BadRequestException($"User with given {propertyName} already exists");
            }
        }

        private async Task CheckInvalidLoginAttemptsNumber(long userId)
        {
            if (await _unitOfWork.UserRepository.GetFailedLoginActivitiesCountInLastHourByUserId(userId) > 3)
            {
                throw new BadRequestException("Too many invalid login attempts. Try again later");
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
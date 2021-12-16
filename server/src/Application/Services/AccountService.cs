using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.InfrastructureInterfaces;
using Application.Utils.Email;
using Application.Utils.Email.Templates;
using Application.Utils.Security;
using Domain.Model;
using Domain.Services;
using Serilog;
using UAParser;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IEmailService _emailService;
        private readonly ISecurityTokenService _securityTokenService;
        private readonly ApplicationSettings _settings;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork, ApplicationSettings settings,
            ISecurityTokenService securityTokenService, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _settings = settings;
            _securityTokenService = securityTokenService;
            _emailService = emailService;
        }

        public async Task RegisterAsync(string email, string username, string password, string encryptionKeyHash,
            string? ipAddress, string? userAgent)
        {
            await CheckIfUserAlreadyExists(email, username);

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password + _settings.PasswordHashPepper, 14);
            var masterPasswordHash =
                BCrypt.Net.BCrypt.HashPassword(encryptionKeyHash + _settings.EncryptionKeyHashPepper, 12);

            var emailVerificationToken = Guid.NewGuid().ToString();
            var emailVerificationTokenHash = BCrypt.Net.BCrypt.HashPassword(emailVerificationToken);
            var emailVerificationTokenValidTo = DateTime.Now.AddHours(1);
            var universalToken = GenerateRandomUniversalToken();

            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            var registeredUser = User.Register(username.ToLower(), email.ToLower(), passwordHash, masterPasswordHash,
                emailVerificationTokenHash, emailVerificationTokenValidTo, universalToken);
            await _unitOfWork.UserRepository.AddAsync(registeredUser);
            await _unitOfWork.SaveChangesAsync();

            var (osName, browserName) = GetDeviceInfo(userAgent);

            registeredUser.AddAccountActivity(ActivityType.SuccessfulRegistration, ipAddress, osName, browserName);

            var url = _settings.FrontendUrl + "/verify-account/" + username + "/" + emailVerificationToken;
            await TryToSendEmailAsync(email, username, url);

            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        public async Task<Auth> LoginAsync(string identifier, string password, string? ipAddress,
            string? userAgent)
        {
            var user = await GetUserIfExistsAndHasVerifiedEmail(identifier);
            // TODO : first check password, then validate if user has verified email 
            await CheckInvalidLoginAttemptsNumberAsync(user.Id);
            var (osName, browserName) = GetDeviceInfo(userAgent);
            await VerifyPassword(password, ipAddress, user, osName, browserName);
            user.AddAccountActivity(ActivityType.SuccessfulLogin, ipAddress, osName, browserName);

            var accessToken = _securityTokenService.GenerateAccessTokenForUser(user.Id, user.Username);
            var refreshToken = _securityTokenService.GenerateRefreshTokenForUser(user.Id, user.Username);

            user.AddSession(refreshToken.TokenGuid, refreshToken.ExpirationTimestamp, ipAddress, osName, browserName);
            await _unitOfWork.SaveChangesAsync();

            return new Auth(accessToken.Token, refreshToken.Token, user.UniversalToken);
        }

        private async Task<User?> GetUserIfExistsAndHasVerifiedEmail(string identifier)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailOrUsernameAsync(identifier.ToLower(),
                identifier.ToLower());
            if (user == default || user.IdEmailConfirmed == false)
            {
                var message = user == default ? "Invalid credentials" : "Email not verified";
                await Task.Delay(ApplicationConstants.InvalidLoginDelayInMilliseconds);
                throw new AuthenticationException(message);
            }

            return user;
        }

        private async Task VerifyPassword(string password, string? ipAddress, User user, string? osName,
            string? browserName)
        {
            var isPasswordVerified =
                BCrypt.Net.BCrypt.Verify(password + _settings.PasswordHashPepper, user.PasswordHash);
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

        private async Task CheckInvalidLoginAttemptsNumberAsync(long userId)
        {
            if (await _unitOfWork.UserRepository.GetFailedLoginActivitiesCountInLastHourByUserId(userId) > 4)
            {
                await Task.Delay(ApplicationConstants.InvalidLoginDelayInMilliseconds);
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

        private async Task TryToSendEmailAsync(string email, string username, string url)
        {
            try
            {
                await _emailService.SendEmailAsync(email, new VerifyEmailAddressEmailTemplateData(username, url));
            }
            catch (Exception exception)
            {
                Log.Error(exception, "Exception occured during sending verification email");
            }
        }

        private static string GenerateRandomUniversalToken()
        {
            var random = new Random();
            return new string(Enumerable
                .Repeat(ApplicationConstants.Alphabet, ApplicationConstants.UniversalTokenLength)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
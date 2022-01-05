using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.InfrastructureInterfaces;
using Application.Utils.Email;
using Application.Utils.Security;
using Application.Utils.UserAgentParser;
using Application.ViewModels;
using Domain.Model;
using FluentValidation;
using MediatR;

namespace Application.Commands
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(command => command.Identifier).NotNull().NotEmpty();
            RuleFor(command => command.Password).NotNull().NotEmpty();
        }
    }

    public class LoginUserCommand : IRequest<AuthViewModel>
    {
        public LoginUserCommand(string identifier, string password, string? ipAddress, string? userAgent)
        {
            Identifier = identifier;
            Password = password;
            IpAddress = ipAddress;
            UserAgent = userAgent;
        }

        public string Identifier { get; }
        public string Password { get; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthViewModel>
    {
        private readonly IEmailService _emailService;
        private readonly ISecurityTokenService _securityTokenService;
        private readonly ApplicationSettings _settings;
        private readonly IUnitOfWork _unitOfWork;

        public LoginUserCommandHandler(IEmailService emailService, ISecurityTokenService securityTokenService,
            ApplicationSettings settings, IUnitOfWork unitOfWork)
        {
            _emailService = emailService;
            _securityTokenService = securityTokenService;
            _settings = settings;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthViewModel> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var user = await GetUserIfExistsEmail(command.Identifier.ToLower());
            await CheckInvalidLoginAttemptsNumberAsync(user.Id);
            var (osName, browserName) = UserAgentParser.GetDeviceInfo(command.UserAgent);
            await VerifyPassword(command.Password, command.IpAddress, user, osName, browserName);
            await CheckIfUserVerifiedEmailAddressAsync(user);
            user.AddAccountActivity(ActivityType.Login, command.IpAddress, osName, browserName);

            var accessToken = _securityTokenService.GenerateAccessTokenForUser(user.Id, user.Username);
            var refreshToken = _securityTokenService.GenerateRefreshTokenForUser(user.Id, user.Username);

            user.AddSession(refreshToken.TokenGuid, refreshToken.ExpirationTimestamp, command.IpAddress, osName,
                browserName);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new AuthViewModel(accessToken.Token, refreshToken.Token, user.Id, user.Username, user.Email);
        }

        private async Task<User> GetUserIfExistsEmail(string identifier)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailOrUsernameAsync(identifier.ToLower(),
                identifier.ToLower());
            if (user == default)
            {
                await Task.Delay(ApplicationConstants.InvalidAuthOperationExtraDelayInMilliseconds);
                throw new AuthenticationException("Invalid credentials");
            }

            return user;
        }

        private static async Task CheckIfUserVerifiedEmailAddressAsync(User user)
        {
            if (user.IdEmailConfirmed == false)
            {
                await Task.Delay(ApplicationConstants.InvalidAuthOperationExtraDelayInMilliseconds);
                throw new AuthenticationException("Email not verified");
            }
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
                await Task.Delay(ApplicationConstants.InvalidAuthOperationExtraDelayInMilliseconds);
                throw new BadRequestException("Invalid credentials");
            }
        }

        private async Task CheckInvalidLoginAttemptsNumberAsync(long userId)
        {
            if (await _unitOfWork.UserRepository.GetFailedLoginActivitiesCountInLastHourByUserIdAsync(userId) > 4)
            {
                await Task.Delay(ApplicationConstants.InvalidAuthOperationExtraDelayInMilliseconds);
                throw new BadRequestException("Too many invalid login attempts. Try again later");
            }
        }
    }
}
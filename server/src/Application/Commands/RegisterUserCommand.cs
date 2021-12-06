using System.Threading;
using System.Threading.Tasks;
using Application.Utils.Email;
using Application.Utils.Email.Templates;
using Application.ViewModels;
using Domain.Services;
using FluentValidation;
using MediatR;

namespace Application.Commands
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(command => command.Email)
                .NotNull()
                .EmailAddress()
                .MinimumLength(ApplicationConstants.MinEmailLength)
                .MaximumLength(ApplicationConstants.MaxEmailLength);
            RuleFor(command => command.Password)
                .NotNull()
                .MinimumLength(ApplicationConstants.MinPasswordLength)
                .MaximumLength(ApplicationConstants.MaxPasswordLength);
            RuleFor(command => command.MasterPassword)
                .NotNull()
                .MinimumLength(ApplicationConstants.MinPasswordLength)
                .MaximumLength(ApplicationConstants.MaxPasswordLength);
            RuleFor(command => command.Username)
                .NotNull()
                .MinimumLength(ApplicationConstants.MinUsernameLength)
                .MaximumLength(ApplicationConstants.MaxUsernameLength);
        }
    }

    public class RegisterUserCommand : IRequest<AuthViewModel>
    {
        public RegisterUserCommand(string username, string email, string password, string masterPassword,
            string? ipAddress, string? userAgent)
        {
            Username = username;
            Email = email;
            Password = password;
            MasterPassword = masterPassword;
            IpAddress = ipAddress;
            UserAgent = userAgent;
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MasterPassword { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthViewModel>
    {
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;

        public RegisterUserCommandHandler(IAccountService accountService, IEmailService emailService)
        {
            _accountService = accountService;
            _emailService = emailService;
        }

        public async Task<AuthViewModel> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var auth = await _accountService.RegisterAsync(command.Email, command.Username, command.Password,
                command.MasterPassword, command.IpAddress, command.UserAgent);

            await _emailService.SendEmailAsync(command.Email, new VerifyEmailAddressEmailTemplateData());

            return new AuthViewModel(auth.AccessToken, auth.RefreshToken);
        }
    }
}
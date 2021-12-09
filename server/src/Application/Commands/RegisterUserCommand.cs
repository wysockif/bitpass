using System.Threading;
using System.Threading.Tasks;
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
                .MaximumLength(ApplicationConstants.MaxPasswordLength)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W])[\S].{0,}$")
                .WithMessage(
                    "'{PropertyName}' must contain at least one uppercase character, one lowercase character, " +
                    "one number, one special character and must not contain any white character.");
            RuleFor(command => command.MasterPassword)
                .NotNull()
                .MinimumLength(ApplicationConstants.MinPasswordLength)
                .MaximumLength(ApplicationConstants.MaxPasswordLength)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W])[\S].{0,}$")
                .WithMessage(
                    "'{PropertyName}' must contain at least one uppercase character, one lowercase character, " +
                    "one number, one special character and must not contain any white character.");
            RuleFor(command => command.Username)
                .NotNull()
                .MinimumLength(ApplicationConstants.MinUsernameLength)
                .MaximumLength(ApplicationConstants.MaxUsernameLength)
                .Matches(@"^[^\s\W]+$")
                .WithMessage("'{PropertyName}' must contain only alphanumeric characters or underscore.");
        }
    }

    public class RegisterUserCommand : IRequest<SuccessViewModel>
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

        public string Username { get; }
        public string Email { get; }
        public string Password { get; }
        public string MasterPassword { get; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, SuccessViewModel>
    {
        private readonly IAccountService _accountService;

        public RegisterUserCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<SuccessViewModel> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            await _accountService.RegisterAsync(command.Email, command.Username, command.Password,
                command.MasterPassword, command.IpAddress, command.UserAgent);

            return new SuccessViewModel();
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Application.ViewModels;
using Domain.Services;
using MediatR;

namespace Application.Commands
{
    public class RegisterAppUserCommand : IRequest<AuthViewModel>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MasterPassword { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }

        public RegisterAppUserCommand(string username, string email, string password, string masterPassword, string? ipAddress, string? userAgent)
        {
            Username = username;
            Email = email;
            Password = password;
            MasterPassword = masterPassword;
            IpAddress = ipAddress;
            UserAgent = userAgent;
        }
    }

    public class RegisterAppUserCommandHandler : IRequestHandler<RegisterAppUserCommand, AuthViewModel>
    {
        private readonly IAccountService _accountService;
        
        public RegisterAppUserCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<AuthViewModel> Handle(RegisterAppUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _accountService.RegisterAsync(command.Email, command.Username, command.Password,
                command.MasterPassword);
            
            var authToken =
                await _accountService.LoginAsync(user.Email, command.Password, command.IpAddress, command.UserAgent);

            return new AuthViewModel { AccessToken = authToken.AccessToken };
        }
    }
}
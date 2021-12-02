using System.Threading;
using System.Threading.Tasks;
using Application.ViewModels;
using Domain.Services;
using MediatR;

namespace Application.Commands
{
    public class LoginUserCommand : IRequest<AuthViewModel>
    {
        public string Identifier { get; set; }
        public string Password { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }

        public LoginUserCommand(string identifier, string password, string? ipAddress, string? userAgent)
        {
            Identifier = identifier;
            Password = password;
            IpAddress = ipAddress;
            UserAgent = userAgent;
        }
    }
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthViewModel>
    {
        private readonly IAccountService _accountService;

        public LoginUserCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<AuthViewModel> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var auth = await _accountService.LoginAsync(command.Identifier, command.Password, command.IpAddress, command.UserAgent);
            return new AuthViewModel(auth.AccessToken);
        }
    }
}
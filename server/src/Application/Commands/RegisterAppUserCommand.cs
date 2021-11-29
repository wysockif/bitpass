using System;
using System.Threading;
using System.Threading.Tasks;
using Application.InfrastructureInterfaces;
using Application.ViewModels;
using MediatR;

namespace Application.Commands
{
    public class RegisterAppUserCommand : IRequest<AuthViewModel>
    {
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MasterPassword { get; set; }
    }

    public class RegisterAppUserCommandHandler : IRequestHandler<RegisterAppUserCommand, AuthViewModel>
    {
        // private readonly IUnitOfWork _unitOfWork;
        //
        // public RegisterAppUserCommandHandler(IUnitOfWork unitOfWork)
        // {
        //     _unitOfWork = unitOfWork;
        // }

        public async Task<AuthViewModel> Handle(RegisterAppUserCommand command, CancellationToken cancellationToken)
        {
            // var appUser = await _unitOfWork.AppUserRepository.GetByEmailOrUsernameAsync(command.Email, command.Username, cancellationToken);
            // if (appUser != default)
            // {
            //     throw new Exception("User with given credentials already exist");
            // }

            // var uaParser = Parser.GetDefault();
            // ClientInfo c = uaParser.Parse(command.UserAgent);
            //
            // Console.WriteLine(c.OS.Family);
            // Console.WriteLine(c.OS.Major);
            //
            // Console.WriteLine(c.UA.Family);
            // Console.WriteLine(c.UA.Major);

            return await Task.FromResult(new AuthViewModel()
            {
                AccessToken = "hello"
            });
        }
    }
}
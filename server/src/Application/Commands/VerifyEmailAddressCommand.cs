using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Application.InfrastructureInterfaces;
using Application.ViewModels;
using Domain.Model;
using MediatR;

namespace Application.Commands
{
    public class VerifyEmailAddressCommand : IRequest<SuccessViewModel>
    {
        public VerifyEmailAddressCommand(string token, string username)
        {
            Token = token;
            Username = username;
        }

        public string Username { get; set; }
        public string Token { get; set; }
    }

    public class VerifyEmailAddressCommandHandler : IRequestHandler<VerifyEmailAddressCommand, SuccessViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public VerifyEmailAddressCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessViewModel> Handle(VerifyEmailAddressCommand command,
            CancellationToken cancellationToken)
        {
            var user = await GetUserIfExistsAsync(command, cancellationToken);

            ValidateToken(command, user.EmailVerificationTokenHash, user.EmailVerificationTokenValidTo);

            user.VerifyEmail();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new SuccessViewModel();
        }

        private static void ValidateToken(VerifyEmailAddressCommand command, string? emailVerificationTokenHash,
            DateTime? emailVerificationTokenValidTo)
        {
            var isTokenValid = emailVerificationTokenHash != default
                               && emailVerificationTokenValidTo != default
                               && BCrypt.Net.BCrypt.Verify(command.Token, emailVerificationTokenHash)
                               && emailVerificationTokenValidTo >= DateTime.Now;

            if (!isTokenValid)
            {
                throw new AuthenticationException("Invalid email confirmation link");
            }
        }

        private async Task<User> GetUserIfExistsAsync(VerifyEmailAddressCommand command,
            CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByUsernameAsync(command.Username, cancellationToken);
            if (user == default)
            {
                throw new AuthenticationException("Invalid email confirmation link");
            }

            return user;
        }
    }
}
using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Application.InfrastructureInterfaces;
using Application.ViewModels;
using Domain.Model;
using FluentValidation;
using MediatR;

namespace Application.Commands
{
    public class VerifyEmailAddressCommandValidator : AbstractValidator<VerifyEmailAddressCommand>
    {
        public VerifyEmailAddressCommandValidator()
        {
            RuleFor(command => command.Username)
                .NotNull()
                .MinimumLength(ApplicationConstants.MinUsernameLength)
                .MaximumLength(ApplicationConstants.MaxUsernameLength);

            RuleFor(command => command.Token)
                .NotNull()
                .Must(token => Guid.TryParse(token, out _))
                .WithMessage("Not valid token");
        }
    }

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
                throw new AuthenticationException($"Invalid token for user {command.Username}");
            }
        }

        private async Task<User> GetUserIfExistsAsync(VerifyEmailAddressCommand command,
            CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByUsernameAsync(command.Username, cancellationToken);
            if (user == default)
            {
                throw new AuthenticationException($"Invalid token for user {command.Username}");
            }

            return user;
        }
    }
}
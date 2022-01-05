using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Application.InfrastructureInterfaces;
using Application.Utils.Security;
using Application.ViewModels;
using Domain.Model;
using FluentValidation;
using MediatR;

namespace Application.Commands
{
    public class RefreshAccessTokenCommandValidator : AbstractValidator<RefreshAccessTokenCommand>
    {
        public RefreshAccessTokenCommandValidator()
        {
            RuleFor(command => command.RefreshToken).NotEmpty().NotEmpty();
        }
    }

    public class RefreshAccessTokenCommand : IRequest<AuthViewModel>
    {
        public RefreshAccessTokenCommand(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public string RefreshToken { get; }
    }

    public class RefreshAccessTokenCommandHandler : IRequestHandler<RefreshAccessTokenCommand, AuthViewModel>
    {
        private readonly ISecurityTokenService _securityTokenService;
        private readonly IUnitOfWork _unitOfWork;

        public RefreshAccessTokenCommandHandler(ISecurityTokenService securityTokenService, IUnitOfWork unitOfWork)
        {
            _securityTokenService = securityTokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthViewModel> Handle(RefreshAccessTokenCommand command, CancellationToken cancellationToken)
        {
            var user = await GetUserFromRefreshToken(command, cancellationToken);
            var oldRefreshTokenGuid = _securityTokenService.GetTokenGuidFromRefreshToken(command.RefreshToken);

            var accessToken = _securityTokenService.GenerateAccessTokenForUser(user.Id, user.Username);
            var refreshToken = _securityTokenService.GenerateRefreshTokenForUser(user.Id, user.Username);

            user.UpdateSession(oldRefreshTokenGuid!.Value, refreshToken.TokenGuid, refreshToken.ExpirationTimestamp);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new AuthViewModel(accessToken.Token, refreshToken.Token, user.Id, user.Username, user.Email);
        }

        private async Task<User> GetUserFromRefreshToken(RefreshAccessTokenCommand command,
            CancellationToken cancellationToken)
        {
            var userId = _securityTokenService.GetUserIdFromRefreshToken(command.RefreshToken);
            if (userId == default)
            {
                throw new AuthenticationException("Provided refresh token is not valid");
            }

            var user = await _unitOfWork.UserRepository.GetByIdIncludingSessionsAsync(userId.Value, cancellationToken);
            if (user == default)
            {
                throw new AuthenticationException("Provided refresh token is not valid");
            }

            return user;
        }
    }
}
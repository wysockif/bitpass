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
    public class LogoutUserCommandValidator : AbstractValidator<LogoutUserCommand>
    {
        public LogoutUserCommandValidator()
        {
            RuleFor(command => command.RefreshToken).NotNull().NotEmpty();
        }
    }

    public class LogoutUserCommand : IRequest<SuccessViewModel>
    {
        public string RefreshToken { get; set; }

        public LogoutUserCommand(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }

    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, SuccessViewModel>
    {
        private readonly ISecurityTokenService _securityTokenService;
        private readonly IUnitOfWork _unitOfWork;

        public LogoutUserCommandHandler(ISecurityTokenService securityTokenService, IUnitOfWork unitOfWork)
        {
            _securityTokenService = securityTokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessViewModel> Handle(LogoutUserCommand command, CancellationToken cancellationToken)
        {
            var user = await GetUserFromRefreshToken(command, cancellationToken);
            var refreshTokenGuid = _securityTokenService.GetTokenGuidFromRefreshToken(command.RefreshToken);
            user.DeleteSession(refreshTokenGuid!.Value);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new SuccessViewModel();
        }

        private async Task<User> GetUserFromRefreshToken(LogoutUserCommand command,
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
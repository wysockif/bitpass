using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.InfrastructureInterfaces;
using Application.Utils.Security;
using Application.ViewModels;
using Domain.Exceptions;
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
        public LogoutUserCommand(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public string RefreshToken { get; }
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
            DeleteSessionIfExists(user, refreshTokenGuid);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new SuccessViewModel();
        }

        private static void DeleteSessionIfExists(User user, Guid? refreshTokenGuid)
        {
            try
            {
                user.DeleteSession(refreshTokenGuid!.Value);
            }
            catch (DomainException exception)
            {
                throw new NotFoundException(exception.Message);
            }
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
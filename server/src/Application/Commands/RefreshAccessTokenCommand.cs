using System;
using System.Threading;
using System.Threading.Tasks;
using Application.InfrastructureInterfaces;
using Application.Utils.Security;
using Application.ViewModels;
using FluentValidation;
using MediatR;

namespace Application.Commands
{
    public class RefreshAccessTokenCommandValidator : AbstractValidator<RefreshAccessTokenCommand>
    {
        public RefreshAccessTokenCommandValidator()
        {
            RuleFor(command => command.RefreshToken).NotEmpty();
        }
    }

    public class RefreshAccessTokenCommand : IRequest<AuthViewModel>
    {
        public string RefreshToken { get; set; }

        public RefreshAccessTokenCommand(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
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
        
        public Task<AuthViewModel> Handle(RefreshAccessTokenCommand command, CancellationToken cancellationToken)
        {
            // var userId = _securityTokenService.GetUserIdFromRefreshToken(command.RefreshToken);
            throw new NotImplementedException();
        }
    }
}
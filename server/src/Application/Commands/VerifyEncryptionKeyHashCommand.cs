using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.InfrastructureInterfaces;
using Application.Utils.Security;
using Application.Utils.UserAgentParser;
using Application.ViewModels;
using Domain.Model;
using FluentValidation;
using MediatR;

namespace Application.Commands
{
    public class VerifyEncryptionKeyHashCommandValidator : AbstractValidator<VerifyEncryptionKeyHashCommand>
    {
        public VerifyEncryptionKeyHashCommandValidator()
        {
            RuleFor(command => command.EncryptionKeyHash).NotNull();
        }
    }

    public class VerifyEncryptionKeyHashCommand : IRequest<SuccessViewModel>
    {
        public string EncryptionKeyHash { get; set; }
        public long UserId { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }

        public VerifyEncryptionKeyHashCommand(string encryptionKeyHash, long userId, string? ipAddress,
            string? userAgent)
        {
            EncryptionKeyHash = encryptionKeyHash;
            UserId = userId;
            IpAddress = ipAddress;
            UserAgent = userAgent;
        }
    }

    public class VerifyEncryptionKeyHashCommandHandler : IRequestHandler<VerifyEncryptionKeyHashCommand, SuccessViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationSettings _settings;


        public VerifyEncryptionKeyHashCommandHandler(IUnitOfWork unitOfWork, ISecurityTokenService securityTokenService, ApplicationSettings settings)
        {
            _unitOfWork = unitOfWork;
            _settings = settings;
        }

        public async Task<SuccessViewModel> Handle(VerifyEncryptionKeyHashCommand command,
            CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(command.UserId, cancellationToken);
            if (user == default)
            {
                throw new NotFoundException("User not found");
            }
            
            var isPasswordVerified = BCrypt.Net.BCrypt.Verify(command.EncryptionKeyHash + _settings.EncryptionKeyHashPepper, user.EncryptionKeyHash);

            if (!isPasswordVerified)
            {
                throw new BadRequestException("Incorrect master password");
            }
            
            return new SuccessViewModel();
        }
    }
}
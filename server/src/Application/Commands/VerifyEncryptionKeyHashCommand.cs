using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.InfrastructureInterfaces;
using Application.Utils.Security;
using Application.ViewModels;
using FluentValidation;
using MediatR;

namespace Application.Commands
{
    public class VerifyEncryptionKeyHashCommandValidator : AbstractValidator<VerifyEncryptionKeyHashCommand>
    {
        public VerifyEncryptionKeyHashCommandValidator()
        {
            RuleFor(command => command.EncryptionKeyHash).NotNull().NotEmpty();
        }
    }

    public class VerifyEncryptionKeyHashCommand : IRequest<SuccessViewModel>
    {
        public VerifyEncryptionKeyHashCommand(string encryptionKeyHash, long userId)
        {
            EncryptionKeyHash = encryptionKeyHash;
            UserId = userId;
        }

        public string EncryptionKeyHash { get; }
        public long UserId { get; set; }
    }

    public class
        VerifyEncryptionKeyHashCommandHandler : IRequestHandler<VerifyEncryptionKeyHashCommand, SuccessViewModel>
    {
        private readonly ApplicationSettings _settings;
        private readonly IUnitOfWork _unitOfWork;

        public VerifyEncryptionKeyHashCommandHandler(IUnitOfWork unitOfWork, ISecurityTokenService securityTokenService,
            ApplicationSettings settings)
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

            var isPasswordVerified =
                BCrypt.Net.BCrypt.Verify(command.EncryptionKeyHash + _settings.EncryptionKeyHashPepper,
                    user.EncryptionKeyHash);

            if (!isPasswordVerified)
            {
                throw new BadRequestException("Incorrect master password");
            }

            return new SuccessViewModel();
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.InfrastructureInterfaces;
using Application.ViewModels;
using Domain.Model;
using MediatR;

namespace Application.Commands
{
    public class AddCipherLoginCommand : IRequest<SuccessViewModel>
    {
        public string EncryptionKeyHash { get; set; }
        public long UserId { get; set; }
        public string WebsiteUrl { get; set; }
        public string Identifier { get; set; }
        public string EncryptedPassword { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }

        public AddCipherLoginCommand(string encryptionKeyHash, long userId, string websiteUrl, string identifier,
            string encryptedPassword, string? ipAddress, string? userAgent)
        {
            EncryptionKeyHash = encryptionKeyHash;
            UserId = userId;
            WebsiteUrl = websiteUrl;
            Identifier = identifier;
            EncryptedPassword = encryptedPassword;
            IpAddress = ipAddress;
            UserAgent = userAgent;
        }
    }

    public class AddCipherLoginCommandHandler : IRequestHandler<AddCipherLoginCommand, SuccessViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationSettings _settings;

        public AddCipherLoginCommandHandler(IUnitOfWork unitOfWork, ApplicationSettings settings)
        {
            _unitOfWork = unitOfWork;
            _settings = settings;
        }

        public async Task<SuccessViewModel> Handle(AddCipherLoginCommand command, CancellationToken cancellationToken)
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

            var cipherLogin = CipherLogin.Create(user.Id, command.Identifier, command.EncryptedPassword, command.WebsiteUrl);
            await _unitOfWork.CipherLoginRepository.AddAsync(cipherLogin, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new SuccessViewModel();
        }
    }
}
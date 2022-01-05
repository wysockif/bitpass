using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.InfrastructureInterfaces;
using Application.Utils.Email;
using Application.Utils.Email.Templates;
using Application.ViewModels;
using FluentValidation;
using MediatR;
using Serilog;

namespace Application.Commands
{
    public class RequestEmailVerificationCommandValidator : AbstractValidator<RequestEmailVerificationCommand>
    {
        public RequestEmailVerificationCommandValidator()
        {
            RuleFor(command => command.Identifier).NotNull().NotEmpty();
        }
    }

    public class RequestEmailVerificationCommand : IRequest<SuccessViewModel>
    {
        public RequestEmailVerificationCommand(string identifier)
        {
            Identifier = identifier;
        }

        public string Identifier { get; }
    }

    public class
        RequestEmailVerificationCommandHandler : IRequestHandler<RequestEmailVerificationCommand, SuccessViewModel>
    {
        private readonly IEmailService _emailService;
        private readonly ApplicationSettings _settings;
        private readonly IUnitOfWork _unitOfWork;

        public RequestEmailVerificationCommandHandler(IUnitOfWork unitOfWork, IEmailService emailService,
            ApplicationSettings settings)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _settings = settings;
        }

        public async Task<SuccessViewModel> Handle(RequestEmailVerificationCommand command,
            CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailOrUsernameAsync(command.Identifier,
                command.Identifier, cancellationToken);
            if (user == default)
            {
                throw new NotFoundException("User not found");
            }

            if (user.IdEmailConfirmed)
            {
                throw new BadRequestException("Email already confirmed");
            }

            var (verificationToken, verificationTokenHash, verificationTokenValidTo) =
                GenerateEmailVerificationToken();
            user.UpdateEmailVerificationToken(verificationTokenHash, verificationTokenValidTo);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await TryToSendEmailAsync(user.Email.ToLower(), user.Username.ToLower(), verificationToken);

            return new SuccessViewModel();
        }

        private static (string, string, DateTime) GenerateEmailVerificationToken()
        {
            var emailVerificationToken = Guid.NewGuid().ToString();
            var emailVerificationTokenHash = BCrypt.Net.BCrypt.HashPassword(emailVerificationToken);
            var emailVerificationTokenValidTo =
                DateTime.Now.AddMinutes(ApplicationConstants.EmailVerificationTokenDurationInMinutes);
            return (emailVerificationToken, emailVerificationTokenHash, emailVerificationTokenValidTo);
        }

        private async Task TryToSendEmailAsync(string email, string username, string verificationToken)
        {
            try
            {
                var url = _settings.FrontendUrl + "/verify-email-address/" + username + "/" + verificationToken;
                await _emailService.SendEmailAsync(email, new VerifyEmailAddressEmailTemplateData(username, url));
            }
            catch (Exception exception)
            {
                Log.Error(exception, "Exception occured during sending verification email");
            }
        }
    }
}
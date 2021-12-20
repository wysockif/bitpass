using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.InfrastructureInterfaces;
using Application.Utils.UserAgentParser;
using Application.ViewModels;
using Domain.Model;
using MediatR;

namespace Application.Commands
{
    public class ResetPasswordCommand : IRequest<SuccessViewModel>
    {
        public ResetPasswordCommand(string username, string resetPasswordToken, string newPassword, string? ipAddress,
            string? userAgent)
        {
            Username = username;
            ResetPasswordToken = resetPasswordToken;
            NewPassword = newPassword;
            IpAddress = ipAddress;
            UserAgent = userAgent;
        }

        public string Username { get; set; }
        public string ResetPasswordToken { get; set; }
        public string NewPassword { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
    }

    public class ResetPasswordCommandValidator : IRequestHandler<ResetPasswordCommand, SuccessViewModel>
    {
        private readonly ApplicationSettings _settings;
        private readonly IUnitOfWork _unitOfWork;

        public ResetPasswordCommandValidator(IUnitOfWork unitOfWork, ApplicationSettings settings)
        {
            _unitOfWork = unitOfWork;
            _settings = settings;
        }


        public async Task<SuccessViewModel> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByUsernameAsync(command.Username, cancellationToken);
            if (user == default)
            {
                throw new NotFoundException("User not found");
            }

            var isTokenValid = BCrypt.Net.BCrypt.Verify(command.ResetPasswordToken, user.PasswordResetTokenHash)
                               && DateTime.Now < user.PasswordResetTokenValidTo;

            var (osName, browserName) = UserAgentParser.GetDeviceInfo(command.UserAgent);
            if (!isTokenValid)
            {
                user.AddAccountActivity(ActivityType.FailedPasswordReset, command.IpAddress, osName, browserName);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await Task.Delay(ApplicationConstants.InvalidAuthOperationExtraDelayInMilliseconds, cancellationToken);
                throw new BadRequestException("Invalid credentials");
            }

            var newPasswordHash =
                BCrypt.Net.BCrypt.HashPassword(command.NewPassword + _settings.PasswordHashPepper, 14);
            user.AddAccountActivity(ActivityType.SuccessfulPasswordReset, command.IpAddress, osName, browserName);
            user.ChangePassword(newPasswordHash);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new SuccessViewModel();
        }
    }
}
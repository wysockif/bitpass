using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.InfrastructureInterfaces;
using Application.ViewModels;
using FluentValidation;
using MediatR;

namespace Application.Commands
{
    public class LogoutAllSessionsCommandValidator : AbstractValidator<LogoutAllSessionsCommand>
    {
        public LogoutAllSessionsCommandValidator()
        {
            RuleFor(command => command.UserId).GreaterThan(0);
        }
    }

    public class LogoutAllSessionsCommand : IRequest<SuccessViewModel>
    {
        public LogoutAllSessionsCommand(long userId)
        {
            UserId = userId;
        }

        public long UserId { get; }
    }

    public class LogoutAllSessionsCommandHandler : IRequestHandler<LogoutAllSessionsCommand, SuccessViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogoutAllSessionsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SuccessViewModel> Handle(LogoutAllSessionsCommand command,
            CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdIncludingSessionsAsync(command.UserId,
                cancellationToken);
            if (user == default)
            {
                throw new NotFoundException("User not found");
            }

            user.DeleteAllSessions();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new SuccessViewModel();
        }
    }
}
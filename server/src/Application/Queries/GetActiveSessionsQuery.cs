using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.InfrastructureInterfaces;
using Application.ViewModels;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Application.Queries
{
    public class GetActiveSessionsQueryValidator : AbstractValidator<GetActiveSessionsQuery>
    {
        public GetActiveSessionsQueryValidator()
        {
            RuleFor(query => query.UserId).GreaterThan(0);
        }
    }

    public class GetActiveSessionsQuery : IRequest<SessionListViewModel>
    {
        public GetActiveSessionsQuery(long userId)
        {
            UserId = userId;
        }

        public long UserId { get; }
    }

    public class GetActiveSessionsQueryHandler : IRequestHandler<GetActiveSessionsQuery, SessionListViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetActiveSessionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SessionListViewModel> Handle(GetActiveSessionsQuery query,
            CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(query.UserId, cancellationToken);
            if (user == default)
            {
                throw new NotFoundException("User not found");
            }

            var sessions = await _unitOfWork.UserRepository
                .GetActiveSessionsByOwnerId(user.Id, cancellationToken);
            return new SessionListViewModel(sessions.Select(session => _mapper.Map<SessionViewModel>(session)));
        }
    }
}
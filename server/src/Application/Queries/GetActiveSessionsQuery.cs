using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.InfrastructureInterfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;

namespace Application.Queries
{
    public class GetActiveSessionsQuery : IRequest<SessionListViewModel>
    {
        public long UserId { get; set; }
    }

    public class GetActiveSessionsQueryHandler : IRequestHandler<GetActiveSessionsQuery, SessionListViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

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
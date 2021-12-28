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
    public class GetAccountActivitiesQuery : IRequest<AccountActivityListViewModel>
    {
        public long UserId { get; set; }
    }

    public class GetAccountActivitiesQueryHandler : IRequestHandler<GetAccountActivitiesQuery, AccountActivityListViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAccountActivitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AccountActivityListViewModel> Handle(GetAccountActivitiesQuery query,
            CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(query.UserId, cancellationToken);
            if (user == default)
            {
                throw new NotFoundException("User not found");
            }

            var activities = await _unitOfWork.UserRepository
                .GetAccountActivitiesByOwnerId(user.Id, 14, cancellationToken);
            return new AccountActivityListViewModel(activities.Select(activity => _mapper.Map<AccountActivityViewModel>(activity)));
        }
    }
}
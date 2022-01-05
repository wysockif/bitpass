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
    public class GetAccountActivitiesQueryValidator : AbstractValidator<GetAccountActivitiesQuery>
    {
        public GetAccountActivitiesQueryValidator()
        {
            RuleFor(query => query.UserId).GreaterThan(0);
        }
    }

    public class GetAccountActivitiesQuery : IRequest<AccountActivityListViewModel>
    {
        public GetAccountActivitiesQuery(long userId)
        {
            UserId = userId;
        }

        public long UserId { get; }
    }

    public class
        GetAccountActivitiesQueryHandler : IRequestHandler<GetAccountActivitiesQuery, AccountActivityListViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

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
            return new AccountActivityListViewModel(activities.Select(activity =>
                _mapper.Map<AccountActivityViewModel>(activity)));
        }
    }
}
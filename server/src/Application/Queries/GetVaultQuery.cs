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
    public class GetVaultQueryValidator : AbstractValidator<GetVaultQuery>
    {
        public GetVaultQueryValidator()
        {
            RuleFor(query => query.UserId).GreaterThan(0);
        }
    }
    
    public class GetVaultQuery : IRequest<VaultViewModel>
    {
        public long UserId { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
    }

    public class GetVaultQueryHandler : IRequestHandler<GetVaultQuery, VaultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVaultQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VaultViewModel> Handle(GetVaultQuery query, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(query.UserId, cancellationToken);
            if (user == default)
            {
                throw new NotFoundException("User not found");
            }

            var vault = await _unitOfWork.CipherLoginRepository.GetByOwnerId(user.Id, cancellationToken);
            return new VaultViewModel(vault.Select(cipherLogin => _mapper.Map<CipherLoginViewModel>(cipherLogin)));
        }
    }

}
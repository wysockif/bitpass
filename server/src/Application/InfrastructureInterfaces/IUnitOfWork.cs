using System.Threading;
using System.Threading.Tasks;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.InfrastructureInterfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ICipherLoginRepository CipherLoginRepository { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    }
}
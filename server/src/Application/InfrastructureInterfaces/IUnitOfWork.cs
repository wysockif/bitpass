using System.Threading;
using System.Threading.Tasks;
using Domain.Repositories;

namespace Application.InfrastructureInterfaces
{
    public interface IUnitOfWork
    {
        IAppUserRepository AppUserRepository { get; }
        ICipherLoginRepository CipherLoginRepository { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
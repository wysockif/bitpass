using System.Threading;
using System.Threading.Tasks;
using Domain.Repositories;

namespace Application.InfrastructureInterfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ICipherLoginRepository CipherLoginRepository { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
using System.Threading;
using System.Threading.Tasks;
using Application.InfrastructureInterfaces;
using Domain.Repositories;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IStorage _storage;
        public IAppUserRepository AppUserRepository { get; }
        public ICipherLoginRepository CipherLoginRepository { get; }

        public UnitOfWork(IStorage storage)
        {
            _storage = storage;
            AppUserRepository = new AppUserRepository(storage);
            CipherLoginRepository = new CipherLoginRepository(storage);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _storage.SaveChangesAsync(cancellationToken);
        }
    }
}
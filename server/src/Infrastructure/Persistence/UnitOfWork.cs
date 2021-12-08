using System.Threading;
using System.Threading.Tasks;
using Application.InfrastructureInterfaces;
using Domain.Repositories;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IStorage _storage;

        public UnitOfWork(IStorage storage)
        {
            _storage = storage;
            UserRepository = new UserRepository(storage);
            CipherLoginRepository = new CipherLoginRepository(storage);
        }

        public IUserRepository UserRepository { get; }
        public ICipherLoginRepository CipherLoginRepository { get; }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _storage.SaveChangesAsync(cancellationToken);
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return _storage.Database.BeginTransactionAsync(cancellationToken);
        }
    }
}
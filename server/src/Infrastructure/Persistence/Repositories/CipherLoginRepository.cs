using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class CipherLoginRepository : ICipherLoginRepository
    {
        private readonly IStorage _storage;

        private IQueryable<CipherLogin> CipherLogins => _storage.CipherLogins.AsQueryable();

        public CipherLoginRepository(IStorage storage)
        {
            _storage = storage;
        }

        public async Task AddAsync(CipherLogin entity, CancellationToken cancellationToken = default)
        {
            await _storage.CipherLogins.AddAsync(entity, cancellationToken);
        }

        public Task DeleteAsync(CipherLogin entity, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_storage.CipherLogins.Remove(entity));
        }

        public async Task<CipherLogin?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await CipherLogins.FirstOrDefaultAsync(login => login.Id == id, cancellationToken);
        }

        public Task<List<CipherLogin>> GetByOwnerId(long ownerId, CancellationToken cancellationToken = default)
        {
            return CipherLogins.Where(login => login.OwnerId == ownerId).ToListAsync(cancellationToken);
        }

        public Task<List<CipherLogin>> GetByOwnerIdAndUrl(long ownerId, string url,
            CancellationToken cancellationToken = default)
        {
            return CipherLogins.Where(login => login.OwnerId == ownerId && url.Contains(login.Url))
                .ToListAsync(cancellationToken);
        }
    }
}
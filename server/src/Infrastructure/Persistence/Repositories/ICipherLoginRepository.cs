using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class CipherLoginRepository : ICipherLoginRepository
    {
        public Task AddAsync(CipherLogin entity, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(CipherLogin entity, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<CipherLogin> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<CipherLogin>> GetByOwnerId(long ownerId, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<CipherLogin>> GetByUrl(string url, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
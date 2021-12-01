using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Repositories.Common.Domain;

namespace Domain.Repositories
{
    public interface ICipherLoginRepository : IRepository<CipherLogin>
    {
        Task<CipherLogin?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<List<CipherLogin>> GetByOwnerId(long ownerId, CancellationToken cancellationToken = default);
        Task<List<CipherLogin>> GetByOwnerIdAndUrl(long ownerId, string url, CancellationToken cancellationToken = default);
    }
}
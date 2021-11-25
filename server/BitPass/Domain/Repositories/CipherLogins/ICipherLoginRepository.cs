using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Model.CipherLogins;
using Domain.Repositories.Common.Domain;

namespace Domain.Repositories.CipherLogins
{
    public interface ICipherLoginRepository : IRepository<CipherLogin>
    {
        Task<CipherLogin> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<CipherLogin>> GetByOwnerId(long ownerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<CipherLogin>> GetByUrl(string url, CancellationToken cancellationToken = default);
    }
}
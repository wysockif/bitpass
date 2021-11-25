using System.Threading;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Repositories.Common.Domain;

namespace Domain.Repositories
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<AppUser> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<AppUser> GetByEmailOrUsernameAsync(string email, string username, CancellationToken cancellationToken = default);
        Task<AppUser> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<AppUser> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    }
}
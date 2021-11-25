using System.Threading;
using System.Threading.Tasks;
using Domain.Model.AppUsers;

namespace Domain.Repositories.AppUsers
{
    public interface IAppUserRepository
    {
        Task<AppUser> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<AppUser> GetByEmailOrUsernameAsync(string email, string username, CancellationToken cancellationToken = default);
        Task<AppUser> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<AppUser> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    }
}
using System.Threading;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Repositories.Common.Domain;

namespace Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<User?> GetByEmailOrUsernameAsync(string email, string username, CancellationToken cancellationToken = default);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<int> GetFailedLoginActivitiesCountInLastHourByUserId(long userId, CancellationToken cancellationToken = default);
    }
}
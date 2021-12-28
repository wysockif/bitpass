using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Repositories.Common.Domain;

namespace Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<User?> GetByIdIncludingSessionsAndActivitiesAsync(long id, CancellationToken cancellationToken = default);
        Task<User?> GetByIdIncludingSessionsAsync(long id, CancellationToken cancellationToken = default);

        Task<User?> GetByEmailOrUsernameAsync(string email, string username,
            CancellationToken cancellationToken = default);

        Task<User?> GetByEmailOrUsernameIncludingSessionsAndActivitiesAsync(string email, string username,
            CancellationToken cancellationToken = default);

        // Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);

        Task<int> GetFailedLoginActivitiesCountInLastHourByUserIdAsync(long userId,
            CancellationToken cancellationToken = default);

        Task<int> GetPasswordResetRequestedActivitiesCountInLastHourByUserIdAsync(long userId,
            CancellationToken cancellationToken = default);
        Task<IEnumerable<AccountActivity>> GetAccountActivitiesByOwnerId(long userId, int days, CancellationToken cancellationToken = default);
        Task<IEnumerable<Session>> GetActiveSessionsByOwnerId(long userId, CancellationToken cancellationToken);
    }
}
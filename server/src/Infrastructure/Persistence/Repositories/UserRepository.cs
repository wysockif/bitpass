using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IStorage _storage;

        public UserRepository(IStorage storage)
        {
            _storage = storage;
        }

        public async Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _storage.Users
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<User?> GetByIdIncludingSessionsAndActivitiesAsync(long id,
            CancellationToken cancellationToken = default)
        {
            return await _storage.Users
                .Include(u => u.Sessions)
                .Include(u => u.AccountActivities)
                .AsSplitQuery()
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<User?> GetByIdIncludingSessionsAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _storage.Users
                .Include(u => u.Sessions)
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<User?> GetByEmailOrUsernameAsync(string email, string username,
            CancellationToken cancellationToken = default)
        {
            return await _storage.Users
                .FirstOrDefaultAsync(u => u.Email.Equals(email.ToLower()) || u.Username.Equals(username.ToLower()),
                    cancellationToken);
        }

        public async Task<User?> GetByEmailOrUsernameIncludingSessionsAndActivitiesAsync(string email, string username,
            CancellationToken cancellationToken = default)
        {
            return await _storage.Users
                .Include(u => u.Sessions)
                .Include(u => u.AccountActivities)
                .AsSplitQuery()
                .FirstOrDefaultAsync(u => u.Email.Equals(email.ToLower()) || u.Username.Equals(username.ToLower()),
                    cancellationToken);
        }

        public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await _storage.Users.FirstOrDefaultAsync(u => u.Username.Equals(username.ToLower()),
                cancellationToken);
        }

        public async Task AddAsync(User entity, CancellationToken cancellationToken = default)
        {
            await _storage.Users.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(User entity, CancellationToken cancellationToken = default)
        {
            await Task.FromResult(_storage.Users.Remove(entity));
        }

        public async Task<int> GetFailedLoginActivitiesCountInLastHourByUserIdAsync(long userId,
            CancellationToken cancellationToken = default)
        {
            return await _storage.AccountActivities
                .Where(activity => activity.UserId == userId
                                   && activity.CreatedAt > DateTime.Now.AddHours(-1)
                                   && activity.ActivityType == ActivityType.FailedLogin)
                .CountAsync(cancellationToken);
        }

        public async Task<int> GetPasswordResetRequestedActivitiesCountInLastHourByUserIdAsync(long userId,
            CancellationToken cancellationToken = default)
        {
            return await _storage.AccountActivities
                .Where(activity => activity.UserId == userId
                                   && activity.CreatedAt > DateTime.Now.AddHours(-1)
                                   && activity.ActivityType == ActivityType.PasswordResetRequested)
                .CountAsync(cancellationToken);
        }

        public async Task<IEnumerable<AccountActivity>> GetAccountActivitiesByOwnerId(long userId, int days,
            CancellationToken cancellationToken = default)
        {
            return await _storage.AccountActivities
                .Where(activity => activity.UserId == userId
                                   && activity.CreatedAt > DateTime.Now.AddDays(-days))
                .OrderByDescending(activity => activity.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Session>> GetActiveSessionsByOwnerId(long userId,
            CancellationToken cancellationToken)
        {
            var nowTimestamp = (DateTimeOffset.Now).ToUnixTimeSeconds();
            return await _storage.Sessions
                .Where(session => session.UserId == userId
                                  && session.ExpirationUnixTimestamp > nowTimestamp)
                .OrderByDescending(session => session.ExpirationUnixTimestamp)
                .ToListAsync(cancellationToken);
        }
    }
}
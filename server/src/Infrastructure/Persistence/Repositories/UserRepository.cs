using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.InfrastructureInterfaces;
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

        public Task<User> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetByEmailOrUsernameAsync(string email, string username,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task AddAsync(User entity, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(User entity, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> GetFailedLoginActivitiesCountInLastHourByUserId(long userId,
            CancellationToken cancellationToken = default)
        {
            return await _storage.AppUserSessions
                .Where(activity => activity.UserId == userId 
                                   && activity.CreatedAt > DateTime.Now.AddHours(-1) 
                                   && activity.ActivityType == ActivityType.FailedLogin)
                .CountAsync(cancellationToken);
        }
    }
}
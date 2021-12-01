using System.Threading;
using System.Threading.Tasks;
using Application.InfrastructureInterfaces;
using Domain.Model;
using Domain.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly IStorage _storage;

        public AppUserRepository(IStorage storage)
        {
            _storage = storage;
        }

        public Task<User> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetByEmailOrUsernameAsync(string email, string username, CancellationToken cancellationToken = default)
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
    }
}
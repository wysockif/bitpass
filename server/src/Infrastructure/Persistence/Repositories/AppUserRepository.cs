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

        public Task<AppUser> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<AppUser> GetByEmailOrUsernameAsync(string email, string username, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<AppUser> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<AppUser> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task AddAsync(AppUser entity, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(AppUser entity, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
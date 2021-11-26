using System.Threading;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public interface IStorage
    {
        DbSet<AppUser> AppUsers { get; }
        DbSet<AppUserSession> AppUserSessions { get; }
        DbSet<CipherLogin> CipherLogins { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
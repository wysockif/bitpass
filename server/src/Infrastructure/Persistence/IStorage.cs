using System.Threading;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public interface IStorage
    {
        DbSet<User> Users { get; }
        DbSet<AccountActivity> AccountActivities { get; }
        DbSet<CipherLogin> CipherLogins { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
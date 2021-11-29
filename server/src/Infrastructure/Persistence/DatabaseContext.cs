using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class DatabaseContext : DbContext, IStorage
    {
        public DbSet<AppUser> AppUsers { get; }
        public DbSet<SecurityActivity> AppUserSessions { get; }
        public DbSet<CipherLogin> CipherLogins { get; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
    }
}
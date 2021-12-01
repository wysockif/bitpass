using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class DatabaseContext : DbContext, IStorage
    {
        public DbSet<User> AppUsers { get; }
        public DbSet<AccountActivity> AppUserSessions { get; }
        public DbSet<CipherLogin> CipherLogins { get; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
    }
}
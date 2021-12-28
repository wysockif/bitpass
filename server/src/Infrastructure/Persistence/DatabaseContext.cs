using System.Reflection;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class DatabaseContext : DbContext, IStorage
    {
        public DbSet<User> Users { get; private set; }
        public DbSet<AccountActivity> AccountActivities { get; private set; }
        public DbSet<CipherLogin> CipherLogins { get; private set; }
        public DbSet<Session> Sessions { get; private set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
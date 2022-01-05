// ReSharper disable UnusedAutoPropertyAccessor.Local

#pragma warning disable 8618

using System.Reflection;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class DatabaseContext : DbContext, IStorage
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; private set; }
        public DbSet<AccountActivity> AccountActivities { get; private set; }
        public DbSet<CipherLogin> CipherLogins { get; private set; }
        public DbSet<Session> Sessions { get; private set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
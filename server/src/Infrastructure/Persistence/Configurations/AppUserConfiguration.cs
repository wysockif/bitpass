using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(appUser => appUser.Id);
            builder.HasIndex(appUser => appUser.Email).IsUnique();
            builder.HasIndex(appUser => appUser.Username).IsUnique();
            builder.HasMany(appUser => appUser.AccountActivities).WithOne().IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
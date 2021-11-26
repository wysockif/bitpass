using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(appUser => appUser.Id);
            builder.HasIndex(appUser => appUser.Email).IsUnique();
            builder.HasIndex(appUser => appUser.Username).IsUnique();
            builder.HasMany(appUser => appUser.Sessions).WithOne().IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
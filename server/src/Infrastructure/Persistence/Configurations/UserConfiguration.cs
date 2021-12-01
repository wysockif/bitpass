using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);
            builder.HasIndex(user => user.Email).IsUnique();
            builder.HasIndex(user => user.Username).IsUnique();
            builder.HasMany(user => user.AccountActivities).WithOne()
                .HasForeignKey(activity => activity.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
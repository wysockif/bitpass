using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AccountActivityConfiguration : IEntityTypeConfiguration<AccountActivity>
    {
        public void Configure(EntityTypeBuilder<AccountActivity> builder)
        {
            builder.HasKey(activity => activity.Id);
            builder.Property(activity => activity.ActivityType).HasConversion<string>();
        }
    }
}
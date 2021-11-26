using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CipherLoginConfiguration : IEntityTypeConfiguration<CipherLogin>
    {
        public void Configure(EntityTypeBuilder<CipherLogin> builder)
        {
            builder.HasKey(cipherLogin => cipherLogin.Id);
        }
    }
}
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gultan.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(u => u.Token)
            .WithOne(t => t.User)
            .HasForeignKey<Token>(t => t.UserId);
    }
}
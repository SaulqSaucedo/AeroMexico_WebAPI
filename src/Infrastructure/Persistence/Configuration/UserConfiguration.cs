using Domain.Users;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasConversion(
            customerId => customerId.Value,
            value => new UserId(value));

        builder.Property(c => c.Name).HasMaxLength(50);

        builder.Property(c => c.LastName).HasMaxLength(50);

        builder.Ignore(c => c.FullName);

        builder.Property(c => c.Email).HasMaxLength(255);

        builder.HasIndex(c => c.Email).IsUnique();

        builder.Property(c => c.Active);
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.Property(e => e.Id).HasColumnName("ID");
        builder.Property(e => e.Email).HasMaxLength(250);
        builder.Property(e => e.FirstName).HasMaxLength(250);
        builder.Property(e => e.LastName).HasMaxLength(250);
    }
}


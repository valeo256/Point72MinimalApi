using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BooksTakenConfiguration : IEntityTypeConfiguration<BooksTaken>
{
    public void Configure(EntityTypeBuilder<BooksTaken> builder)
    {
        builder
            .HasNoKey()
            .ToTable("BooksTaken");

        builder.Property(e => e.BookId).HasColumnName("BookID");
        builder.Property(e => e.DateTaken).HasColumnType("datetime");
        builder.Property(e => e.UserId).HasColumnName("UserID");

        builder.HasOne(d => d.Book).WithMany()
            .HasForeignKey(d => d.BookId)
            .HasConstraintName("FK_BooksTaken_Book");

        builder.HasOne(d => d.User).WithMany()
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_BooksTaken_User");
    }
}
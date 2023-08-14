using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Book");

        builder.Property(e => e.Id).HasColumnName("ID");
        builder.Property(e => e.AuthorId).HasColumnName("AuthorID");
        builder.Property(e => e.Description).HasMaxLength(1000);
        builder.Property(e => e.Title).HasMaxLength(250);

        builder.HasOne(d => d.Author).WithMany(p => p.Books)
            .HasForeignKey(d => d.AuthorId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Book_Author");
    }
}


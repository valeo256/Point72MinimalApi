using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Author> Authors { get; set; }

    DbSet<Book> Books { get; set; }

    DbSet<BooksTaken> BooksTakens { get; set; }

    DbSet<User> Users { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

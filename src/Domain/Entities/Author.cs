namespace Domain.Entities;

public class Author
{
    public long Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public virtual ICollection<Book> Books { get; private set; } = new List<Book>();
}

namespace Application.Requests.BookHandlers.Queries;

public class BookVm
{
    public long Id { get; init; }

    public string Title { get; set; } = default!;

    public string? Description { get; init; }

    public long AuthorId { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Entities.Book, BookVm>();
        }
    }
}


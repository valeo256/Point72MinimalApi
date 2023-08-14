using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Requests.BookHandlers.Queries;

public record GetBookQuery(long Id) : IRequest<BookVm>;
public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetBookQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BookVm> Handle(GetBookQuery request, CancellationToken cancellation)
    {
        var book = await _context.Books
            .AsNoTracking()
            .ProjectTo<BookVm>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (book == null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }

        return book;
    }
}
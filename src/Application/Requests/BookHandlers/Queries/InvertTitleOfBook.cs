using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Utils;
using Domain.Entities;

namespace Application.Requests.BookHandlers.Queries;

public record InvertTitleOfBook(long Id) : IRequest<BookVm>;
public class InvertTitleOfBookHandler : IRequestHandler<InvertTitleOfBook, BookVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public InvertTitleOfBookHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BookVm> Handle(InvertTitleOfBook request, CancellationToken cancellation)
    {
        var bookVm = await _context.Books
            .AsNoTracking()
            .ProjectTo<BookVm>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (bookVm == null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }

        var invertedTitle = StringFormatUtils.InvertWordsInSentence(bookVm.Title);

        bookVm.Title = invertedTitle;

        return bookVm;
    }
}
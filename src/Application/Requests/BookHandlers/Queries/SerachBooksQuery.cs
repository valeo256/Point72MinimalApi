using Application.Common.Interfaces;
using Application.Common.Utils;
using Domain.Enums;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Requests.BookHandlers.Queries;

public record SerachBooksQuery : IRequest<List<BookVm>>
{
    public string? AuthorName { get; init; }
    public string? SearchText { get; init; }
    public long? UserId { get; init; }
    public required CombinationConditionOfEntities CombinationCondition { get; init; }
}

public class SerachBooksQueryHandler : IRequestHandler<SerachBooksQuery, List<BookVm>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public SerachBooksQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<BookVm>> Handle(SerachBooksQuery request, CancellationToken cancellation)
    {
        var query = _context.Books.AsQueryable();

        var authorName = request.AuthorName;
        var searchText = request.SearchText;
        var userId = request.UserId;
        var combinationCondition = request.CombinationCondition;

        Expression<Func<Book, bool>> filterExpression = book => combinationCondition == CombinationConditionOfEntities.And;

        if (!string.IsNullOrWhiteSpace(authorName))
        {
            Expression<Func<Book, bool>> authorExpression = book =>
                book.Author.FirstName.Contains(authorName)
                || book.Author.LastName.Contains(authorName)
                || (book.Author.MiddleName != null && book.Author.MiddleName.Contains(authorName));
            filterExpression = ExpressionUtils.CombineExpressions(filterExpression, authorExpression, combinationCondition);
        }

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            Expression<Func<Book, bool>> searchExpression = book =>
                book.Title.Contains(searchText)
                || (book.Description != null && book.Description.Contains(searchText));
            filterExpression = ExpressionUtils.CombineExpressions(filterExpression, searchExpression, combinationCondition);
        }

        if (userId.HasValue)
        {
            Expression<Func<Book, bool>> userExpression = book =>
                _context.BooksTakens.Any(bt => bt.BookId == book.Id && bt.UserId == userId);
            filterExpression = ExpressionUtils.CombineExpressions(filterExpression, userExpression, combinationCondition);
        }

        query = query
            .AsNoTracking()
            .Where(filterExpression);

        var result = await query.ProjectTo<BookVm>(_mapper.ConfigurationProvider).ToListAsync();

        return result;
    }
}

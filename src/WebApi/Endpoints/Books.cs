using Application.Common.Utils;
using Application.Requests.BookHandlers.Queries;
using Domain.Enums;
using MediatR;
using WebApi.Infrastructure;

namespace WebApi.Endpoints;

public class Books : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this).MapGet("/invertwords/{id}", InvertWordsForTitle)
            .AllowAnonymous()
            .Produces(200, typeof(BookVm))
            .Produces(404);
        app.MapGroup(this).MapGet("/search", SearchBooks)
            .AllowAnonymous()
            .Produces(200, typeof(BookVm))
            .Produces(404);
    }

    public async Task<BookVm> InvertWordsForTitle(ISender sender, long id)
    {
        var bookVm = await sender.Send(new GetBookQuery(id));
        var invertedTitle = StringFormatUtils.InvertWordsInSentence(bookVm.Title);

        bookVm.Title = invertedTitle;

        return bookVm;
    }

    public async Task<IEnumerable<BookVm>> SearchBooks(ISender sender, string? authorName, 
        string? searchText, long? userId, 
        CombinationConditionOfEntities combinationCondition = CombinationConditionOfEntities.And)
    {
        var booksVm = await sender.Send(new SerachBooksQuery { AuthorName = authorName, 
            SearchText = searchText, UserId = userId, CombinationCondition = combinationCondition });

        return booksVm;
    }
}

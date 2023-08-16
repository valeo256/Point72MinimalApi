using Application.Requests.BookHandlers.Queries;
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
        var bookVm = await sender.Send(new InvertTitleOfBook(id));

        return bookVm;
    }

    public async Task<IEnumerable<BookVm>> SearchBooks(ISender sender,
        [AsParameters] SerachBooksQuery query)
    {
        var booksVm = await sender.Send(query);

        return booksVm;
    }
}

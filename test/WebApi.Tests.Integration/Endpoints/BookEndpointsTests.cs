using Application.Requests.BookHandlers.Queries;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NSubstitute;
using System.Net;

namespace WebApi.Tests.Integration.Endpoints;

public class BookEndpointsTests
{
    private readonly ISender _sender =
            Substitute.For<ISender>();

    [Fact]
    public async Task GetBookByIdWithInvertWordsForTitle()
    {
        //Arrange
        var id = 1;
        var book = new BookVm { Id = 1, AuthorId = 1, Title = "Test title", Description = "Test description" };
        var bookVm = _sender.Send(new GetBookQuery(id)).Returns(book);

        using var app = new TestApplicationFactory(x =>
        {
            x.AddSingleton(_sender);
        });

        var httpClient = app.CreateClient();

        //Act
        var response = await httpClient.GetAsync($"api/books/invertwords/{id}");
        var responseText = await response.Content.ReadAsStringAsync();
        var customerResult = JsonConvert.DeserializeObject<BookVm>(responseText);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        customerResult?.Title.Should().Be("title Test");
    }
}

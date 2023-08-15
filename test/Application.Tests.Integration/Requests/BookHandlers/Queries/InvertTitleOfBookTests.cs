using Application.Common.Exceptions;
using Application.Requests.BookHandlers.Queries;
using AutoMapper;
using FluentAssertions;
using Infrastructure.Persistence;
using Xunit;

namespace Application.Tests.Integration.Requests.BookHandlers.Queries;

[Collection("QueryCollection")]
public class InvertTitleOfBookTests
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public InvertTitleOfBookTests(QueryTestFixture fixture)
    {
        _context = fixture.Context;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task ShouldReturnSpecificBook()
    {
        var sut = new InvertTitleOfBookHandler(_context, _mapper);

        var result = await sut.Handle(new InvertTitleOfBook(1), CancellationToken.None);

        result.Should().BeOfType<BookVm>();
        result.Title.Should().Be("Raven The");
    }


    [Fact]
    public async Task ShouldReturnSNotFoundError()
    {
        var sut = new InvertTitleOfBookHandler(_context, _mapper);

        var act = async () => { await sut.Handle(new InvertTitleOfBook(0), CancellationToken.None); };
        await act.Should().ThrowAsync<NotFoundException>();
    }
}

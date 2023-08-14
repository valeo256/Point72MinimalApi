using Application.Common.Exceptions;
using Application.Requests.BookHandlers.Queries;
using AutoMapper;
using FluentAssertions;
using Infrastructure.Persistence;
using Xunit;

namespace Application.Tests.Integration.Requests.BookHandlers.Queries;

[Collection("QueryCollection")]
public class GetBookQueryTests
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBookQueryTests(QueryTestFixture fixture)
    {
        _context = fixture.Context;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task ShouldReturnSpecificBook()
    {
        var sut = new GetBookQueryHandler(_context, _mapper);

        var result = await sut.Handle(new GetBookQuery(1), CancellationToken.None);

        result.Should().BeOfType<BookVm>();
        result.Title.Should().BeSameAs("The Raven");
    }


    [Fact]
    public async Task ShouldReturnSNotFoundError()
    {
        var sut = new GetBookQueryHandler(_context, _mapper);

        var act = async () => { await sut.Handle(new GetBookQuery(0), CancellationToken.None); };
        await act.Should().ThrowAsync<NotFoundException>();
    }
}

using Application.Requests.BookHandlers.Queries;
using AutoMapper;
using Domain.Enums;
using FluentAssertions;
using Infrastructure.Persistence;
using Xunit;

namespace Application.Tests.Integration.Requests.BookHandlers.Queries;

[Collection("QueryCollection")]
public class SerachBooksQueryTests
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public SerachBooksQueryTests(QueryTestFixture fixture)
    {
        _context = fixture.Context;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task ShouldReturnOneSpecificBookByTitleAndConditionOr()
    {
        var sut = new SerachBooksQueryHandler(_context, _mapper);

        var result = await sut.Handle(new SerachBooksQuery
        {
            SearchText = "A Farewell to Arms",
            CombinationCondition = CombinationConditionOfEntities.Or
        },
            CancellationToken.None);

        result.Should().BeOfType<List<BookVm>>();
        result.Should().HaveCount(1);
        result.First().Title.Should().Be("A Farewell to Arms");
    }


    [Fact]
    public async Task ShouldReturnOneSpecificBookByTitleWithConditionAnd()
    {
        var sut = new SerachBooksQueryHandler(_context, _mapper);

        var result = await sut.Handle(new SerachBooksQuery
        {
            SearchText = "A Farewell to Arms",
            CombinationCondition = CombinationConditionOfEntities.And
        },
            CancellationToken.None);

        result.Should().BeOfType<List<BookVm>>();
        result.Should().HaveCount(1);
        result.First().Title.Should().Be("A Farewell to Arms");
    }

    [Fact]
    public async Task ShouldReturnOneSpecificBookByDescriptionWithConditionAnd()
    {
        var sut = new SerachBooksQueryHandler(_context, _mapper);

        var result = await sut.Handle(new SerachBooksQuery
        {
            SearchText = "A narrative poem by American",
            CombinationCondition = CombinationConditionOfEntities.And
        },
            CancellationToken.None);

        result.Should().BeOfType<List<BookVm>>();
        result.Should().HaveCount(1);
        result.First().Id.Should().Be(1);
    }

    [Fact]
    public async Task ShouldReturnOneSpecificBookByAuthorsLastNameWithConditionAnd()
    {
        var sut = new SerachBooksQueryHandler(_context, _mapper);

        var result = await sut.Handle(new SerachBooksQuery
        {
            AuthorName = "Hemingway",
            CombinationCondition = CombinationConditionOfEntities.And
        },
            CancellationToken.None);

        result.Should().BeOfType<List<BookVm>>();
        result.Should().HaveCount(1);
        result.First().Id.Should().Be(2);
    }

    [Fact]
    public async Task ShouldReturnTwoSpecificBooksByUserOrAuthorCondition()
    {
        var sut = new SerachBooksQueryHandler(_context, _mapper);

        var result = await sut.Handle(new SerachBooksQuery
        {
            UserId = 2,
            AuthorName = "Poe",
            CombinationCondition = CombinationConditionOfEntities.Or
        },
            CancellationToken.None);

        result.Should().HaveCount(2);
        result.Should().Contain(x => x.Id == 2);
        result.Should().Contain(x => x.Id == 1);
    }

    [Fact]
    public async Task ShouldReturnNullSpecificBooksByUserAndAuthorCondition()
    {
        var sut = new SerachBooksQueryHandler(_context, _mapper);

        var result = await sut.Handle(new SerachBooksQuery
        {
            UserId = 2,
            AuthorName = "Poe",
            CombinationCondition = CombinationConditionOfEntities.And
        },
            CancellationToken.None);

        result.Should().HaveCount(0);
    }
}

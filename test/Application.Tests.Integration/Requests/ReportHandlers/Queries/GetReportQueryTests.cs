using AutoMapper;
using Infrastructure.Persistence;
using Xunit;

namespace Application.Tests.Integration.Requests.ReportHandlers.Queries;

[Collection("QueryCollection")]
public class GetReportQueryTests
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetReportQueryTests(QueryTestFixture fixture)
    {
        _context = fixture.Context;
        _mapper = fixture.Mapper;
    }

    //TODO: Cannot run due to SqlServerDbFunctionsExtensions.DateDiffDay not found
    //[Fact]
    //public async Task ShouldReturnDetailsOfUserAndBookStatistics()
    //{
    //    var sut = new GetReportQueryHandler(_context, _mapper);

    //    var result = await sut.Handle(new GetReportQuery(), CancellationToken.None);

    //    result.Should().BeOfType<List<ReportVm>>();
    //    result.Should().HaveCount(2);
    //    result.Should().Contain(x => x.UserDetails.FirstName == "Lars");
    //    result.Should().Contain(x => x.UserDetails.LastName == "Hetfield");
    //    result.First(x => x.UserDetails.FirstName == "Lars").TotalBooksTaken.Should().Be(1);
    //    result.First(x => x.UserDetails.FirstName == "James").TotalBooksTaken.Should().Be(2);
    //}
}

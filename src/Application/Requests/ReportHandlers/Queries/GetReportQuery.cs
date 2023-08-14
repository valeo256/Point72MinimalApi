using Application.Common.Interfaces;

namespace Application.Requests.ReportHandlers.Queries;

public record GetReportQuery : IRequest<List<ReportVm>>;
public class GetReportQueryHandler : IRequestHandler<GetReportQuery, List<ReportVm>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetReportQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ReportVm>> Handle(GetReportQuery request, CancellationToken cancellation)
    {
        var currentDate = DateTime.Now.Date;

        var report = await _context.Users
        .AsNoTracking()
        .GroupJoin(
                _context.BooksTakens,
                user => user.Id,
                booksTaken => booksTaken.UserId,
                (user, booksTakenGroup) => new ReportVm
                {
                    UserDetails = _mapper.Map<UserDetailsReportDto>(user),
                    TotalBooksTaken = booksTakenGroup.Count(),
                    TotalDaysHoldingBooks = booksTakenGroup.Sum(booksTaken =>
                        EF.Functions.DateDiffDay(booksTaken.DateTaken, currentDate))
                })
            .ToListAsync();

        return report;
    }
}
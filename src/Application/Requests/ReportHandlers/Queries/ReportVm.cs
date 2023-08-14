namespace Application.Requests.ReportHandlers.Queries;

public class ReportVm
{
    public required UserDetailsReportDto UserDetails { get; init; }
    public int TotalBooksTaken { get; init; }
    public int TotalDaysHoldingBooks { get; init; }
}

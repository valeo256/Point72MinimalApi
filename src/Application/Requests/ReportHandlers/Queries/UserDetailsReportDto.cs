using Application.Common.Mapping;

namespace Application.Requests.ReportHandlers.Queries;

public class UserDetailsReportDto : IMapFrom<Domain.Entities.User>
{
    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string Email { get; init; }
}

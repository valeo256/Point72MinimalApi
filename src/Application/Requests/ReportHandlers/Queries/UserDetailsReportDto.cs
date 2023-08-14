using Application.Common.Mapping;
using Domain.Entities;

namespace Application.Requests.ReportHandlers.Queries;

public class UserDetailsReportDto : IMapFrom<User>
{
    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string Email { get; init; }
}

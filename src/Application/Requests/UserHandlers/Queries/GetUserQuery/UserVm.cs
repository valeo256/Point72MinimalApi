namespace Application.Requests.UserHandlers.Queries.GetUserQuery;

public class UserVm
{
    public Guid UserId { get; init; } = Guid.NewGuid();
    public string FullName { get; init; } = default!;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Entities.UserExample, UserVm>().ForMember(d => d.UserId,
            opt => opt.MapFrom(s => s.Id));
        }
    }
}

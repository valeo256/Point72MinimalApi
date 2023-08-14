using Application.Common.Interfaces;

namespace Application.Requests.UserHandlers.Queries.GetUserQuery;

public record GetUserQuery : IRequest<UserVm>
{
    public Guid Id { get; set; }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserVm>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    public GetUserQueryHandler(IUserService service, IMapper mapper)
    {
        _userService = service;
        _mapper = mapper;
    }

    public async Task<UserVm> Handle(GetUserQuery request, CancellationToken cancellation)
    {
        var customer = _userService.GetById(request.Id);
        return await Task.FromResult(_mapper.Map<UserVm>(customer));
    }
}
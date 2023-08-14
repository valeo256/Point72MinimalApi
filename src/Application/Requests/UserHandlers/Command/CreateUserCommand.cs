using Application.Common.Interfaces;
using Domain.Events;

namespace Application.Requests.UserHandlers.Command;

public record CreateUserCommand : IRequest<Guid>
{
    public Guid Id { get; init; }
    public required string FullName { get; init; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserService _userService;
    public CreateUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.UserExample { Id = command.Id, FullName = command.FullName };
        entity.AddDomainEvent(new UserCreatedEvent(entity));

        //_context.TodoItems.Add(entity);

        //await _context.SaveChangesAsync(cancellationToken);
        _userService.Create(new Domain.Entities.UserExample { Id = command.Id, FullName = command.FullName });

        return await Task.FromResult(entity.Id);
    }
}

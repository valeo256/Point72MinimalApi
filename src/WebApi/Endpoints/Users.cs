using Application.Common.Interfaces;
using Application.Requests.UserHandlers.Command;
using Application.Requests.UserHandlers.Queries.GetUserQuery;
using MediatR;
using WebApi.Infrastructure;

namespace WebApi.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this).MapGet("/user/{id}", GetUserById)
            .AllowAnonymous()
            .Produces(200, typeof(UserVm))
            .Produces(404);

        app.MapGroup(this).MapPost("/user", CreateUser)
        .AllowAnonymous()
        .Produces<CreateUserCommand>()
        .Produces(400, typeof(IDictionary<string, string[]>));
    }

    public async Task<UserVm> GetUserById(ISender sender, Guid id)
    {
        return await sender.Send(new GetUserQuery { Id = id});
    }

    public async Task<Guid> CreateUser(ISender sender, CreateUserCommand command)
    {
        return await sender.Send(command);
    }
}

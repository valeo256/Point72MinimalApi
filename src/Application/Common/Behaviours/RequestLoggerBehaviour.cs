using MediatR.Pipeline;
using Application.Common.Interfaces;

namespace Application.Common.Behaviours;
public class RequestLoggerBehaviour<TRequest> :
    IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;

    public RequestLoggerBehaviour(ILogger<TRequest> logger,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var name = typeof(TRequest).Name;

        _logger.LogInformation("Request: {Name} {@UserId} {@Request}",
            name, _currentUserService.UserId, request);

        return Task.CompletedTask;
    }
}
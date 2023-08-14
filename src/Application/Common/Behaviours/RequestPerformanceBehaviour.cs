using Application.Common.Interfaces;
using System.Diagnostics;

namespace Application.Common.Behaviours;

public class RequestPerformanceBehaviour<TRequest, TResponse> : 
    IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;

    public RequestPerformanceBehaviour(ILogger<TRequest> logger,
        ICurrentUserService currentUserService)
    {
        _timer = new Stopwatch();

        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            var name = typeof(TRequest).Name;

            _logger.LogWarning("Running Request: {Name} " +
                "({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}",
                name, elapsedMilliseconds, _currentUserService.UserId, request);
        }

        return response;
    }
}
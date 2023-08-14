using Application.Requests.BookHandlers.Queries;
using Application.Requests.ReportHandlers.Queries;
using MediatR;
using WebApi.Infrastructure;

namespace WebApi.Endpoints;

public class Reports : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this).MapGet("/report", BuildReport)
            .AllowAnonymous()
            .Produces(200, typeof(BookVm))
            .Produces(404);
    }

    public async Task<List<ReportVm>> BuildReport(ISender sender)
    {
        var report = await sender.Send(new GetReportQuery());
        return report;
    }
}


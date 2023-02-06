using People.Application.People;

namespace People.API.Endpoints.People;

internal static class DownloadRelatedPeopleReportEndpoint
{
    internal static async Task<IResult> ExecuteAsync(
        IPeopleService peopleService,
        CancellationToken cancellationToken)
    {
        var (content, contentType, fileName) = await peopleService
            .GetRelatedPeopleReportStreamAsync(cancellationToken);

        return Results.File(content, contentType, fileName);
    }
}

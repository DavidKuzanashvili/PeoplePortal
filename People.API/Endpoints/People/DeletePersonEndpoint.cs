using People.Application.People;

namespace People.API.Endpoints.People;

internal sealed class DeletePersonEndpoint
{
    internal static async Task<IResult> ExecuteAsync(
        Guid personId,
        IPeopleService peopleService,
        CancellationToken cancellationToken)
    {
        await peopleService.DeletePersonAsync(personId, cancellationToken);
        return Results.Ok();
    }
}

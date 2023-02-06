using People.API.Endpoints.People.Contracts;
using People.Application.People;
using People.Application.People.Dtos;

namespace People.API.Endpoints.People;

internal sealed class AssingRelatedPeopleEndpoint
{
    internal static async Task<IResult> ExecuteAsync(
        AssignRelatedPeopleRequest request,
        IPeopleService peopleService,
        CancellationToken cancellationToken)
    {
        var dto = Transform(request);
        await peopleService.AssingRelatedPeopleAsync(dto, cancellationToken);
        return Results.Ok();
    }

    private static AssignPeopleDto Transform(
        AssignRelatedPeopleRequest req)
    {
        return new AssignPeopleDto(
            req.Id,
            req.People
                .Select(x => new AssignedPersonItemDto(x.Id, x.RelationType))
                .ToList());
    }
}
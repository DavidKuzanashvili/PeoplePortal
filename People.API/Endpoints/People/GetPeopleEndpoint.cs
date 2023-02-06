using People.API.Endpoints.People.Contracts;
using People.Application.Common;
using People.Application.People;
using People.Application.People.Dtos;

namespace People.API.Endpoints.People;

internal sealed class GetPeopleEndpoint
{
    internal static async Task<IResult> ExecuteAsync(
        string? sort,
        int? skip,
        int? take,
        IPeopleService peopleService,
        CancellationToken cancellationToken)
    {
        var result = await peopleService.GetFilteredReadonlyAsync(
            sort,
            skip,
            take,
            cancellationToken);

        return Results.Ok(Transform(result));
    }

    internal static GetPeopleResponse Transform(GetPeopleDto dto)
    {
        return new GetPeopleResponse(
            dto.People
                .Select(x => new GetPeopleItemResponse(
                    x.Id,
                    x.Name,
                    x.Surname,
                    x.PersonalNumber))
                .ToList(),
            dto.TotalCount);
    }
}

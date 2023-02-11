using People.API.Endpoints.People.Contracts;
using People.Application.People;
using People.Application.People.Dtos;

namespace People.API.Endpoints.People;

internal sealed class GetPersonEndpoint
{
    internal static async Task<IResult> ExecuteAsync(
        Guid personId,
        IPeopleService peopleService,
        CancellationToken cancellationToken)
    {
        var result = await peopleService.GetByIdReadonlyAsync(personId, cancellationToken);

        if (result is null)
            return Results.NotFound();

        return Results.Ok(Transform(result));
    }

    internal static GetPersonResponse Transform(GetPersonDto dto)
    {
        return new GetPersonResponse(
            dto.Id,
            dto.Name,
            dto.Surname,
            dto.Gender,
            dto.CityName,
            dto.PersonalNumber,
            dto.DateOfBirth,
            dto.ImagePath,
            dto.PhoneNumbers
                .Select(x => new PhoneNumberItem(
                    x.Type,
                    x.CountryCode,
                    x.PhoneCode))
                .ToList(),
            dto.AssignedPeople
                .Select(x => new AssignedPersonItem(x.Id, x.RelationType))
                .ToList());
    }
}

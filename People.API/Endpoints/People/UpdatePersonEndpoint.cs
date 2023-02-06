using People.API.Endpoints.People.Contracts;
using People.Application.People;
using People.Application.People.Dtos;

namespace People.API.Endpoints.People;

internal sealed class UpdatePersonEndpoint
{
    internal static async Task<IResult> ExecuteAsync(
        UpdatePersonRequest req,
        IPeopleService peopleService,
        CancellationToken cancellationToken)
    {
        await peopleService.UpdatePersonAsync(Transform(req), cancellationToken);
        return Results.Ok();
    }

    internal static UpdatePersonDto Transform(UpdatePersonRequest req)
    {
        return new UpdatePersonDto(
            req.Id,
            req.Name,
            req.Surname,
            req.Gender,
            req.CityName,
            req.PersonalNumber,
            req.DateOfBirth,
            req.PhoneNumbers
                .Select(x => new PhoneNumberItemDto(x.Type, x.CountryCode, x.PhoneCode))
                .ToList());
    }
}

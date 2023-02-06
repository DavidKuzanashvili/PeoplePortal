using People.API.Endpoints.People.Contracts;
using People.API.Filters;
using People.Application.People;
using People.Application.People.Dtos;

namespace People.API.Endpoints.People;

internal sealed class CreatePersonEndpoint
{
    internal static async Task<IResult> ExecuteAsync(
        [Validate] CreatePersonRequest request,
        IPeopleService peopleService,
        CancellationToken cancellationToken)
    {
        var dto = Transform(request);
        var result = await peopleService.CreatePersonAsync(dto, cancellationToken);
        return Results.Ok(result);
    }

    private static CreatePersonDto Transform(CreatePersonRequest req)
    {
        return new CreatePersonDto(
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

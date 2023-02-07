namespace People.API.Endpoints.People.Contracts;

public record GetPeopleQuery(
    string? Query,
    string? Name,
    string? Surname,
    string? Gender,
    string? PhoneNumber,
    string? CityName,
    string? PersonalNumber);

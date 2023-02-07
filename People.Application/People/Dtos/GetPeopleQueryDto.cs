namespace People.Application.People.Dtos;

public record GetPeopleQueryDto(
    string? Query,
    string? Name,
    string? Surname,
    string? Gender,
    string? PhoneNumber,
    string? CityName,
    string? PersonalNumber);
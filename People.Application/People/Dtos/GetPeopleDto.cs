namespace People.Application.People.Dtos;

public record GetPeopleDto(
    List<GetPeopleItemDto> People,
    int TotalCount);

public record GetPeopleItemDto(
    Guid Id,
    string Name,
    string Surname,
    string PersonalNumber);

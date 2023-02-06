namespace People.API.Endpoints.People.Contracts;

public record GetPeopleResponse(
    List<GetPeopleItemResponse> People,
    int TotalCount);

public record GetPeopleItemResponse(
    Guid Id,
    string Name,
    string Surname,
    string PersonalNumber);

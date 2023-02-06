namespace People.API.Endpoints.People.Contracts;

public record GetPersonResponse(
    Guid Id,
    string Name,
    string Surname,
    string Gender,
    string CityName,
    string PersonalNumber,
    DateTime DateOfBirth,
    List<PhoneNumberItem> PhoneNumbers,
    List<AssignedPersonItem> AssingedPeople);

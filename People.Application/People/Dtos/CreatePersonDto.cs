namespace People.Application.People.Dtos;

public record CreatePersonDto(
    string Name,
    string Surname,
    string Gender,
    string CityName,
    string PersonalNumber,
    DateTime DateOfBirth,
    List<PhoneNumberItemDto> PhoneNumbers);

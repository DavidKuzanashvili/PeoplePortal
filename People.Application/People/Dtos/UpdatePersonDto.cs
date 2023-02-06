namespace People.Application.People.Dtos;

public record UpdatePersonDto(
    Guid Id,
    string Name,
    string Surname,
    string Gender,
    string CityName,
    string PersonalNumber,
    DateTime DateOfBirth,
    List<PhoneNumberItemDto> PhoneNumbers);
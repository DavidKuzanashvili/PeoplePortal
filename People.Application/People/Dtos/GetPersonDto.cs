namespace People.Application.People.Dtos;

public record GetPersonDto(
    Guid Id,
    string Name,
    string Surname,
    string Gender,
    string CityName,
    string PersonalNumber,
    DateTime DateOfBirth,
    string ImagePath,
    List<PhoneNumberItemDto> PhoneNumbers,
    List<AssignedPersonItemDto> AssignedPeople);

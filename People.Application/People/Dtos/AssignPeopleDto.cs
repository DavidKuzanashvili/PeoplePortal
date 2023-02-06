namespace People.Application.People.Dtos;

public record AssignPeopleDto(Guid Id, List<AssignedPersonItemDto> People);

public record AssignedPersonItemDto(Guid Id, string RelationType);

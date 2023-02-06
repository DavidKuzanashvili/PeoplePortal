namespace People.API.Endpoints.People.Contracts;

public record AssignRelatedPeopleRequest(Guid Id, List<AssignedPersonItem> People);

public record AssignedPersonItem(Guid Id, string RelationType);

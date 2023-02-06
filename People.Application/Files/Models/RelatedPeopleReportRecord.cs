using People.Domain.Enums;

namespace People.Application.Files.Models;

public class RelatedPeopleReportRecord
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string PersonalNumber { get; set; } = null!;
    public int RelatedPeopleCount { get; set; }
    public RelatedPersonType RelationType { get; set; }
}

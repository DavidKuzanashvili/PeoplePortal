using People.Domain.Enums;

namespace People.Application.Files.Models;

public class RelatedPeopleReportRecord
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string PersonalNumber { get; set; } = null!;
    public int Colleagues{ get; set; }
    public int Relatives{ get; set; }
    public int Friends{ get; set; }
    public int Others { get; set; }
}

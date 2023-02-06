using People.Application.Files.Models;
using People.Domain.Entities;

namespace People.Application.Files;

public interface ICsvFileBuilder
{
    byte[] BuildRelatedPeopleReportFile(IEnumerable<RelatedPeopleReportRecord> records);
}


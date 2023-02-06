using CsvHelper.Configuration;
using People.Application.Files.Models;
using System.Globalization;

namespace People.Infrastructure.Files.Maps;

public class RelatedPeopleReportRecordMap : ClassMap<RelatedPeopleReportRecord>
{
    public RelatedPeopleReportRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
    }
}

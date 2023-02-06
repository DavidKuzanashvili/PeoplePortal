using CsvHelper;
using People.Application.Files;
using People.Application.Files.Models;
using People.Infrastructure.Files.Maps;
using System.Globalization;

namespace People.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildRelatedPeopleReportFile(IEnumerable<RelatedPeopleReportRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
            csvWriter.Context.RegisterClassMap<RelatedPeopleReportRecordMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
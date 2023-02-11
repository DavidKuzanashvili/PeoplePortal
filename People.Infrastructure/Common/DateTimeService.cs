using People.Application.Common;

namespace People.Infrastructure.Common;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}

using People.Application.Common;
using People.Domain.Entities;

namespace People.Application.Cities.Repositories;

public interface ICitiesRepository : IRepository<City, Guid>
{
    Task<City?> GetByNameAsync(string name, CancellationToken cancellationToken);
}

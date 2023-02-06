using Microsoft.EntityFrameworkCore;
using People.Application.Cities.Repositories;
using People.Domain.Entities;
using People.Infrastructure.Persistence.Context;

namespace People.Infrastructure.Persistence.Repositories;

public class CitiesRepository : 
    Repository<City, Guid>, ICitiesRepository
{
    public CitiesRepository(PeopleContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<City?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _set.FirstOrDefaultAsync(x => x.Name.Equals(name), cancellationToken);
    }
}

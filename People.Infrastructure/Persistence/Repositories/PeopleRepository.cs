using Microsoft.EntityFrameworkCore;
using People.Application.Common;
using People.Application.People.Repositories;
using People.Domain.Entities;
using People.Infrastructure.Persistence.Context;

namespace People.Infrastructure.Persistence.Repositories;

public class PeopleRepository : 
    Repository<Person, Guid>, IPeopleRepository
{
	public PeopleRepository(PeopleContext dbContext) 
		: base(dbContext)
	{
	}

    public async Task<Person?> GetAggregateAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _set
            .Include(x => x.City)
            .Include(x => x.PhoneNumbers)
            .Include(x => x.RelatedPeople)
            .FirstOrDefaultAsync(x => x.EntityId == id, cancellationToken);
    }

    public async Task<Person?> GetByIdReadonlyAsync(
        Guid id, 
        CancellationToken cancellationToken)
    {
        return await _readonlySet
            .Include(x => x.City)
            .Include(x => x.PhoneNumbers)
            .Include(x => x.RelatedPeople)
            .FirstOrDefaultAsync(x => x.EntityId == id, cancellationToken);
    }

    public async Task<List<Person>> GetByIdsReadonlyAsync(
        IEnumerable<Guid> ids, 
        CancellationToken cancellationToken)
    {
        return await _readonlySet
            .Where(x => ids.Contains(x.EntityId))
            .ToListAsync(cancellationToken);
    }

    public async Task<(List<Person>, int)> GetFilteredReadonlyAsync(
        SortingOrder sort, 
        int skip, 
        int take, 
        CancellationToken cancellationToken)
    {
        var result = await _readonlySet
            .Include(x => x.City)
            .Include(x => x.PhoneNumbers)
            .Include(x => x.RelatedPeople)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);
        var totalCount = await _readonlySet.CountAsync(cancellationToken);
        return (result, totalCount);
    }

    public async Task SetImagePathAsync(
        Guid id,
        string path, 
        CancellationToken cancellationToken)
    {
        var person = await _set.FirstOrDefaultAsync(x => x.EntityId == id, cancellationToken);
        if (person == null) return;
        person.SetImagePath(path);
    }
}

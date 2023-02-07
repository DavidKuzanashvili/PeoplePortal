using Microsoft.EntityFrameworkCore;
using People.Application.Common;
using People.Application.People.Dtos;
using People.Application.People.Repositories;
using People.Domain.Entities;
using People.Infrastructure.Extensions;
using People.Infrastructure.Persistence.Context;
using People.Infrastructure.Persistence.Extensions;
using System.Runtime.InteropServices;

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
        GetPeopleQueryDto filter,
        CancellationToken cancellationToken)
    {
        var query = _readonlySet
            .Include(x => x.City)
            .Include(x => x.PhoneNumbers)
            .Include(x => x.RelatedPeople)
            .SortBy(x => x.Name, sort)
            .AsQueryable();

        FilterReadOnly(query, filter);
        
        var result = await  query
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);
        var totalCount = await _readonlySet.CountAsync(cancellationToken);
        return (result, totalCount);
    }

    public Task<List<Person>> GetWithRelatedPeopleReadOnlyAsyn(CancellationToken cancellationToken)
    {
        return _readonlySet
            .Include(x => x.RelatedPeople)
            .ToListAsync(cancellationToken);

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

    private void FilterReadOnly(
        IQueryable<Person> query,
        GetPeopleQueryDto filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.Query))
        {
            query = query
                .Where(x =>
                    x.Name.ContinsIgnoreCase(filter.Query)
                    || x.Surname.ContinsIgnoreCase(filter.Query)
                    || x.PersonalNumber.ContinsIgnoreCase(filter.Query)
                    || x.City.Name.ContinsIgnoreCase(filter.Query)
                    || x.Gender.ToString().ContinsIgnoreCase(filter.Query)
                    || x.PhoneNumbers.Any(pn => pn.PhoneCode.ContinsIgnoreCase(filter.Query)));
        }

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query
                .Where(x => x.Name
                    .ContinsIgnoreCase(filter.Name));
        }

        if (!string.IsNullOrWhiteSpace(filter.Surname))
        {
            query = query
                .Where(x => x.Surname
                    .ContinsIgnoreCase(filter.Surname));
        }

        if (!string.IsNullOrWhiteSpace(filter.PersonalNumber))
        {
            query = query
                .Where(x => x.PersonalNumber
                    .ContinsIgnoreCase(filter.PersonalNumber));
        }

        if (!string.IsNullOrWhiteSpace(filter.CityName))
        {
            query = query
                .Where(x => x.City.Name
                    .ContinsIgnoreCase(filter.CityName));
        }

        if (!string.IsNullOrWhiteSpace(filter.Gender))
        {
            query = query
                .Where(x => x.Gender.ToString()
                    .ContinsIgnoreCase(filter.Gender));
        }

        if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
        {
            query = query
                .Where(x => x.PhoneNumbers
                    .Any(pn => pn.PhoneCode.ContinsIgnoreCase(filter.PhoneNumber)));
        }
    }
}

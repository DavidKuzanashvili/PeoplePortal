using Microsoft.EntityFrameworkCore;
using People.Application.Common;
using People.Domain.Common;
using People.Infrastructure.Persistence.Context;

namespace People.Infrastructure.Persistence.Repositories;

public class Repository<T, TId> : IRepository<T, TId>
    where T : class, IEntity<TId>
    where TId : struct, IEquatable<TId>, IComparable<TId>
{
    protected readonly PeopleContext _dbContext;
    protected readonly DbSet<T> _set;
    protected readonly IQueryable<T> _readonlySet;

    public Repository(PeopleContext dbContext)
    {
        _dbContext = dbContext;
        _set = _dbContext.Set<T>();
        _readonlySet = _dbContext.Set<T>().AsNoTracking();
    }

    public void Add(T entity)
    {
        _set.Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _set.AddRange(entities);
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _set.ToListAsync(cancellationToken);
    }

    public async Task<T?> GetAsync(TId id, CancellationToken cancellationToken)
    {
        return await _set.FirstOrDefaultAsync(x => x.EntityId.Equals(id), cancellationToken);
    }

    public void Remove(T entity)
    {
        _set.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _set.RemoveRange(entities);
    }
}

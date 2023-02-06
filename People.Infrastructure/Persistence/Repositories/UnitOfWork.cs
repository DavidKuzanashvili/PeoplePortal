using People.Application.Common;
using People.Infrastructure.Persistence.Context;

namespace People.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly PeopleContext _dbContext;

    public UnitOfWork(PeopleContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CompleteAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

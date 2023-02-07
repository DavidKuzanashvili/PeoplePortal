using People.Application.Common;
using System.Linq.Expressions;

namespace People.Infrastructure.Persistence.Extensions;
internal static class QueryableExtensions
{
    internal static IOrderedQueryable<T> SortBy<T, TKey>(
        this IQueryable<T> query,
        Expression<Func<T, TKey>> selector,
        SortingOrder order)
    {
        return order is SortingOrder.Descending
            ? query.OrderByDescending(selector)
            : query.OrderBy(selector);
    }
}

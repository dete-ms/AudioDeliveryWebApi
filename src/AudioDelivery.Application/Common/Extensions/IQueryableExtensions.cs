using AudioDelivery.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace AudioDelivery.Application.Common.Extensions;

public static class IQueryableExtensions
{
    public static async Task<PaginatedResult<T>> ToPaginatedResultAsync<T>(
        this IQueryable<T> query, 
        int offset, 
        int limit,
        string href,
        CancellationToken cancellationToken = default)
    {
        var total = await query.CountAsync(cancellationToken);
        var items = await query.Skip(offset).Take(limit).ToListAsync(cancellationToken);

        return new PaginatedResult<T>
        {
            Items = items,
            Total = total,
            Limit = limit,
            Offset = offset,
            Href = href,
            Next = offset + limit < total ? $"{href}?offset={offset + limit}&limit={limit}" : null,
            Previous = offset > 0 ? $"{href}?offset={Math.Max(0, offset - limit)}&limit={limit}" : null
        };
    }
}

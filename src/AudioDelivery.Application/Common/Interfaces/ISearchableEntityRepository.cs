using AudioDelivery.Application.Search.DTOs;

namespace AudioDelivery.Application.Common.Interfaces;

public interface ISearchableEntityRepository
{
    /// <summary>
    /// Asynchronously searches for items that match the specified query and returns the results in a paged format.
    /// </summary>
    /// <param name="query">The search query string used to filter items. Cannot be null or empty.</param>
    /// <param name="limit">The maximum number of items to return in the result set. Must be greater than or equal to 1. The default is 50.</param>
    /// <param name="offset">The number of items to skip before starting to collect the result set. Must be greater than or equal to 0. The
    /// default is 0.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="SearchResultDto"/> with
    /// the search results. If no items match the query, the results will contain empty collections.</returns>
    Task<SearchResultDto> SearchAsync(string query, int limit = 50, int offset = 0, CancellationToken cancellationToken = default);
}

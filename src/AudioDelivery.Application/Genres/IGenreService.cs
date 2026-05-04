namespace AudioDelivery.Application.Genres;

/// <summary>
/// Service interface for Genre operations. Only admins should be able to create, update, or delete genres, while all users should be able to read them.
/// Genres are generated through the DataSeeder or directly in the database, so no create/update/delete operations are exposed through the API. 
/// The service currently only supports retrieving all genre names, but it can be extended in the future if needed.
/// </summary>
public interface IGenreService
{
    /// <summary>
    /// GET /recommendations/genres/names - Asynchronously retrieves a list of all available genre names.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of strings, each representing
    /// a genre name. The list will be empty if no genres are available.</returns>
    Task<List<string>> GetAllGenreNamesAsync(CancellationToken cancellationToken = default);
}

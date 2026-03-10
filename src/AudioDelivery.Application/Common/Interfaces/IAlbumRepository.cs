using AudioDelivery.Application.Albums.DTOs;
using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Application.Common.Interfaces;

/// <summary>
/// Album-specific repository interface.
/// Extends IRepository<Album> with queries specific to the Albums domain.
/// </summary>
public interface IAlbumRepository : IRepository<Album>
{
    /// <summary>
    /// Creates a new album using the specified album creation request.
    /// </summary>
    /// <param name="createAlbumRequest">The details of the album to create. Must not be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="AlbumDto"/> with the
    /// album details if it was created successfully; otherwise, <see langword="null"/> if the album is not found.</returns>
    Task<AlbumDto?> CreateAlbum(CreateAlbumRequest createAlbumRequest);

    /// <summary>
    /// Updates the details of an existing album with the specified values.
    /// </summary>
    /// <param name="id">The unique identifier of the album to update.</param>
    /// <param name="updateAlbumRequest">An object containing the updated album information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="AlbumDto"/> with the
    /// updated album details if the update is successful; otherwise, <see langword="null"/> if the album is not found.</returns>
    Task<AlbumDto?> UpdateAlbum(Guid id, UpdateAlbumRequest updateAlbumRequest);

    /// <summary>
    /// Deletes the album with the specified unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the album to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains
    /// <see langword="true"/> if the album was found and deleted; otherwise, <see langword="false"/>.</returns>
    Task<bool> DeleteAlbum(Guid id);
}

using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Domain.JoinTables;

/// <summary>
/// Join entity for the many-to-many relationship between Artist and Genre.
/// </summary>
public class ArtistGenre
{
    public Guid ArtistId { get; set; }
    public Artist Artist { get; set; } = null!;

    public Guid GenreId { get; set; }
    public Genre Genre { get; set; } = null!;
}

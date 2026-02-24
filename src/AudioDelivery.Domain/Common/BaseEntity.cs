namespace AudioDelivery.Domain.Common;

/// <summary>
/// Base class for all domain entities.
/// Provides a shared primary key (Id), audit timestamps (CreatedAt, UpdatedAt),
/// and a concurrency token (RowVersion) that every table will inherit.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Unique identifier for the entity.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// UTC timestamp of when this record was first created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// UTC timestamp of the most recent update.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}

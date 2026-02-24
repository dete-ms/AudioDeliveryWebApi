using System.Linq.Expressions;
using AudioDelivery.Domain.Common;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Generic repository interface providing basic CRUD operations for any entity.
///
/// WHY USE A REPOSITORY?
///   - Abstracts away the data access details (EF Core) from the service layer.
///   - Makes unit testing easier – you can mock IRepository&lt;T&gt; in tests.
///   - Centralizes query logic and avoids scattering DbContext calls everywhere.
///
/// PATTERN:
///   Each domain area has its own specific repository interface (e.g., IAlbumRepository)
///   that inherits from IRepository&lt;T&gt; and adds domain-specific query methods.
///
/// TODO: Implement this interface in Repository&lt;T&gt; using AppDbContext.
/// </summary>
/// <typeparam name="T">The entity type, must inherit from BaseEntity.</typeparam>
public interface IRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Get an entity by its primary key (Guid Id).
    /// </summary>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all entities of this type. Use sparingly on large tables.
    /// </summary>
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Find entities matching a predicate expression.
    /// </summary>
    Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a new entity to the context (not yet saved to DB).
    /// </summary>
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Mark an entity as modified.
    /// </summary>
    void Update(T entity);

    /// <summary>
    /// Mark an entity for deletion.
    /// </summary>
    void Delete(T entity);

    /// <summary>
    /// Check if any entity matches the predicate.
    /// </summary>
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Save all pending changes to the database.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

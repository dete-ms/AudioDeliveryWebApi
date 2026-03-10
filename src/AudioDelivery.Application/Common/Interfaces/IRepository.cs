using AudioDelivery.Domain.Common;
using AudioDelivery.Domain.Entities;
using System.Linq.Expressions;

namespace AudioDelivery.Application.Common.Interfaces;

/// <summary>
/// Generic repository interface providing basic CRUD operations for any entity.
/// </summary>
/// <typeparam name="T">The entity type, must inherit from BaseEntity.</typeparam>
public interface IRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Returns a queryable collection of entities of type T that can be further filtered, ordered, and projected using
    /// LINQ.
    /// </summary>
    /// <remarks>The returned query supports deferred execution. Additional LINQ operators can be applied to
    /// customize the query before execution. Changes to the underlying data source after obtaining the query may affect
    /// the results when the query is executed.</remarks>
    /// <returns>An <see cref="IQueryable{T}"/> representing the collection of entities. The query is not executed until the
    /// result is enumerated.</returns>
    IQueryable<T> Query();

    /// <summary>
    /// Gets an entity by its primary key.
    /// </summary>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an entity by its primary key, projected to the specified DTO type.
    /// </summary>
    /// <typeparam name="TDto">The type of the data transfer object to which the entity will be mapped.</typeparam>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the mapped data transfer object if
    /// the entity is found; otherwise, <see langword="null"/>.</returns>
    Task<TDto?> GetByIdAsync<TDto>(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all entities of this type. Use sparingly on large tables.
    /// </summary>
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds entities matching a predicate expression.
    /// </summary>
    Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Find entities matching a predicate expression, projected to the specified DTO type.
    /// </summary>
    /// <typeparam name="TDto">The type to which the matching entities will be projected. Must be a data transfer object (DTO) type compatible
    /// with the entity.</typeparam>
    Task<List<TDto>> FindAsync<TDto>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

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

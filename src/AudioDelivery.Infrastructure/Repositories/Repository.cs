using System.Linq.Expressions;
using AudioDelivery.Domain.Common;
using AudioDelivery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Generic repository implementation using EF Core.
///
/// This class provides the default CRUD operations for any entity type.
/// Domain-specific repositories (e.g., AlbumRepository) inherit from this
/// and add their own specialized query methods.
///
/// TODO: Review each method and understand how it maps to EF Core operations.
///       Pay attention to:
///       - When tracking is needed vs. AsNoTracking() for read-only queries
///       - The difference between Add (inserts) and Update (marks as modified)
///       - SaveChangesAsync commits ALL pending changes in the context
/// </summary>
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        // TODO: Consider whether to use FindAsync (uses cache) or
        //       SingleOrDefaultAsync with AsNoTracking for read-only scenarios.
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}

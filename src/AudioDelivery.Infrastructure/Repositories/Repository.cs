using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Infrastructure.Exceptions;
using AudioDelivery.Infrastructure.Data;
using AudioDelivery.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Generic repository implementation using EF Core.
///
/// This class provides the default CRUD operations for any entity type.
/// Domain-specific repositories (e.g., AlbumRepository) inherit from this
/// and add their own specialized query methods.
/// </summary>
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext _context;
    protected readonly IMapper _mapper;
    protected readonly DbSet<T> _dbSet;

    public Repository(
        AppDbContext context, 
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = context.Set<T>();
    }

    public IQueryable<T> Query() => _dbSet.AsNoTracking();

    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        // TODO: Consider whether to use FindAsync (uses cache) or
        //       SingleOrDefaultAsync with AsNoTracking for read-only scenarios.
        return _dbSet.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public Task<TDto?> GetByIdAsync<TDto>(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbSet.AsNoTracking().Where(t => t.Id == id).ProjectTo<TDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _dbSet.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
    }

    public Task<List<TDto>> FindAsync<TDto>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _dbSet.AsNoTracking().Where(predicate).ProjectTo<TDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), $"Cannot add a null entity of type {typeof(T).Name} to the DB.");
        }

        try
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }
        catch (DbUpdateException)
        {
            throw new EntityAdditionFailedException($"Something went wrong with the addition of {entity.Id} of type {typeof(T).Name} to the DB.");
        }
    }

    public void Update(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), $"Cannot update a null entity of type {typeof(T).Name} in the DB.");
        }

        try        
        {
            _dbSet.Update(entity);
        }
        catch (DbUpdateException)
        {
            throw new EntityUpdateFailedException($"Something went wrong with the update of {entity.Id} of type {typeof(T).Name} in the DB.");
        }
    }

    public void Delete(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), $"Cannot delete a null entity of type {typeof(T).Name} from the DB.");
        }

        try       
        {
            _dbSet.Remove(entity);
        }
        catch (DbUpdateException)
        {
            throw new EntityDeletionFailedException($"Something went wrong with the deletion of {entity.Id} of type {typeof(T).Name} from the DB.");
        }
    }

    public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _dbSet.AnyAsync(predicate, cancellationToken);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}

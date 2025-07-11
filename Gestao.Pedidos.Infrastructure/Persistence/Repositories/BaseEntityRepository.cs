using Gestao.Pedidos.Core.Repositories;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;

namespace Gestao.Pedidos.Infrastructure.Persistence.Repositories;

public class BaseEntityRepository<TEntity, TId>(
    DbContext context,
    DbSet<TEntity> dbSet
    ) : IBaseEntityRepository<TEntity, TId>, IAsyncDisposable
    where TEntity : class
    where TId : struct
{
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await dbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await dbSet.AddRangeAsync(entities, cancellationToken);
        return entities;
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await dbSet
            .AsNoTracking()
            .AnyAsync(predicate, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = dbSet;
        query = IncludeProperties(includeProperties, query);
        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetByAndOrderedByAsync<TKey>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TKey>> orderBy,
        bool ascending = true,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = dbSet.Where(predicate);
        query = IncludeProperties(includeProperties, query);
        query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetOrderedByAsync<TKey>(
        Expression<Func<TEntity, TKey>> orderBy,
        bool ascending = true,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = dbSet;
        query = IncludeProperties(includeProperties, query);
        query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetByAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = dbSet.Where(predicate);
        query = IncludeProperties(includeProperties, query);
        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<TEntity> GetSingleByAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = dbSet.Where(predicate);
        query = IncludeProperties(includeProperties, query);
        return await query
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity> GetByIdAsync(
        TId id,
        string idPropertyName = "Id",
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = dbSet;
        query = IncludeProperties(includeProperties, query);
        return await query
            .AsNoTracking()
            .FirstOrDefaultAsync(e => EF.Property<TId>(e, idPropertyName).Equals(id), cancellationToken);
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        dbSet.Update(entity);
        return Task.FromResult(entity);
    }

    public Task RemoveAsync(TEntity entity)
    {
        dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    async Task IBaseEntityRepository<TEntity, TId>.DisposeAsync()
    {
        await context.DisposeAsync();
    }

    static IQueryable<TEntity> IncludeProperties(
        Expression<Func<TEntity, object>>[] includeProperties,
        IQueryable<TEntity> query)
    {
        if (includeProperties == null || includeProperties.Length == 0)
            return query;

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return query;
    }

    public async Task<(IEnumerable<TEntity> Items, int TotalRecords)> GetPagedAsync(
        Expression<Func<TEntity, bool>> filter,
        int page,
        int pageSize,
        string? orderBy = null,
        bool ascending = true,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = context.Set<TEntity>()
            .AsNoTracking()
            .Where(filter);

        foreach (var include in includes)
            query = query.Include(include);

        var totalRecords = await query.CountAsync();

        if (!string.IsNullOrWhiteSpace(orderBy))
            query = query.OrderBy($"{orderBy} {(ascending ? "asc" : "desc")}");

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalRecords);
    }
}

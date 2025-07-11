using System.Linq.Expressions;

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

    public async Task RemoveAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        context.Set<TEntity>()
            .RemoveRange(entities);               

        await Task.Yield();
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
}

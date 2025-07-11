using System.Linq.Expressions;

namespace Gestao.Pedidos.Core.Repositories;

public interface IBaseEntityRepository<TEntity, in TId>
    where TEntity : class
    where TId : struct
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task RemoveAsync(TEntity entity);

    Task RemoveAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

    Task<(IEnumerable<TEntity> Items, int TotalRecords)> GetPagedAsync(
        Expression<Func<TEntity, bool>> filter, int page, int pageSize, string? orderBy = null, bool ascending = true,
        CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);

    Task<IEnumerable<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

    Task<IEnumerable<TEntity>> GetByAndOrderedByAsync<TKey>(
        Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, bool ascending = true,
        CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

    Task<IEnumerable<TEntity>> GetOrderedByAsync<TKey>(
        Expression<Func<TEntity, TKey>> orderBy, bool ascending = true,
        CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

    Task<TEntity> GetSingleByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

    Task<TEntity> GetByIdAsync(TId id, string idPropertyName = "Id", CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

    Task SaveAsync(CancellationToken cancellationToken = default);

    Task DisposeAsync();
}
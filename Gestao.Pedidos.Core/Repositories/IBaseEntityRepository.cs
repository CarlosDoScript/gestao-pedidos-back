using System.Linq.Expressions;

namespace Gestao.Pedidos.Core.Repositories;

public interface IBaseEntityRepository<TEntity, in TId>
    where TEntity : class
    where TId : struct
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task RemoveAsync(TEntity entity);

    Task RemoveAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task<TEntity> GetByIdAsync(TId id, string idPropertyName = "Id", CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

    Task SaveAsync(CancellationToken cancellationToken = default);

    Task DisposeAsync();
}
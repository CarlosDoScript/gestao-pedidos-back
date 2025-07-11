namespace Gestao.Pedidos.Core.Repositories;

public interface IOrderRepository : IBaseEntityRepository<Order,int>
{
    Task<Order?> GetByIdAndItemsAsync(int id);
}
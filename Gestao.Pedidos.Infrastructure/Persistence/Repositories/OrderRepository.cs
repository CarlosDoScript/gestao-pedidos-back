
namespace Gestao.Pedidos.Infrastructure.Persistence.Repositories;

public class OrderRepository(
        GestaoPedidosDbContext context
    ) : BaseEntityRepository<Order, int>(
        context, context.Set<Order>()
    ), IOrderRepository
{
    public async Task<Order?> GetByIdAndItemsAsync(int id)
    {
        return await
            context.Order
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
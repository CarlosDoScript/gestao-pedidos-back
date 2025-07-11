namespace Gestao.Pedidos.Infrastructure.Persistence.Repositories;

public class OrderItemRepository(
        GestaoPedidosDbContext context
    ) : BaseEntityRepository<OrderItem,int>(
            context, context.Set<OrderItem>()
        ), IOrderItemRepository
{
}
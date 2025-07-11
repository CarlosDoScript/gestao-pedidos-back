namespace Gestao.Pedidos.Infrastructure.Persistence.Repositories;

public class OrderRepository (
        GestaoPedidosDbContext context
    ) : BaseEntityRepository<Order,int>(
        context, context.Set<Order>()    
    ), IOrderRepository
{
}
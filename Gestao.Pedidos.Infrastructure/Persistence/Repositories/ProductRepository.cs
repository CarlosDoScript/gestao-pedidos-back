namespace Gestao.Pedidos.Infrastructure.Persistence.Repositories;

public class ProductRepository(
        GestaoPedidosDbContext context
    ) : BaseEntityRepository<Product,int>(
        context,context.Set<Product>()
      ), IProductRepository
{
}
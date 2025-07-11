namespace Gestao.Pedidos.Infrastructure.Persistence.Repositories;

public class CustomerRepository(
        GestaoPedidosDbContext context
    ) : BaseEntityRepository<Customer,int>(
        context, context.Set<Customer>()
    ), ICustomerRepository
{
}
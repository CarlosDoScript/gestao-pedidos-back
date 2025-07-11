namespace Gestao.Pedidos.Application.Queries.Order.GetByIOrder;

public class GetByIdOrderQuery : IRequest<Resultado<OrderDocument>>
{
    public int OrderId { get; set; }
}

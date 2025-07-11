namespace Gestao.Pedidos.Application.Queries.Order.GetByIOrder;

public class GetByIdOrderQuery : IRequest<Resultado<OrderViewModel>>
{
    public int OrderId { get; set; }
}

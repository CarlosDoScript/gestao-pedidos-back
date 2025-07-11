namespace Gestao.Pedidos.Application.Commands.Order.UpdateOrder;

public class UpdateOrderCommand : IRequest<Resultado<OrderViewModel>>
{
    internal int OrderId { get; private set; }

    public int CustomerId { get; set; }
    public List<OrderItemInputModel> Items { get; set; } = new();

    public void SetId(int id)
        => OrderId = id;
}

namespace Gestao.Pedidos.Application.Commands.Order.DeleteOrder;

public class DeleteOrderCommand : IRequest<Resultado>
{
    public int OrderId { get; init; }

    public DeleteOrderCommand(int orderId)
    {
        OrderId = orderId;
    }
}

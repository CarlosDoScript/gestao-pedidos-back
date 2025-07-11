namespace Gestao.Pedidos.Application.Commands.Order.CreateOrder;

public class CreateOrderCommand : IRequest<Resultado<OrderViewModel>>
{
    public int CustomerId { get; set; }
    public List<OrderItemInputModel> Items { get; set; } = new();
}
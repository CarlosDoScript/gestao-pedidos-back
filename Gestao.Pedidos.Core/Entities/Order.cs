namespace Gestao.Pedidos.Core.Entities;

public class Order : BaseEntity
{
    private  Order(){}

    public int CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public Quantity TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }

    public virtual Customer Customer { get; private set; }

    public List<OrderItem> Items { get; private set; }
}

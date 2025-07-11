namespace Gestao.Pedidos.Core.Entities;

public class OrderItem : BaseEntity
{
    public OrderItem() {}

    public int OrderId { get; private set; }
    public int ProductId { get; private set; }
    public string ProductName { get; private set; }
    public Quantity Quantity { get; private set; }
    public Money UnitPrice { get; private set; }
    public Money TotalPrice { get; private set; }

    public virtual Order Order { get; private set; }
    public virtual Product Product { get; private set; }    
}

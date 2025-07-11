namespace Gestao.Pedidos.Core.Entities;

public class OrderItem : BaseEntity
{
    public OrderItem() {}

    OrderItem(int orderId, 
        int productId,
        string productName,
        Quantity quantity, 
        Money unitPrice, 
        Money totalPrice
    )
    {
        OrderId = orderId;
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalPrice = totalPrice;
    }

    public int OrderId { get; private set; }
    public int ProductId { get; private set; }
    public string ProductName { get; private set; }
    public Quantity Quantity { get; private set; }
    public Money UnitPrice { get; private set; }
    public Money TotalPrice { get; private set; }

    public virtual Order Order { get; private set; }
    public virtual Product Product { get; private set; }    

    public static Resultado<OrderItem> Create(
        int orderId,
        int productId,
        string productName,
        int quantity,
        decimal unitPrice,
        decimal totalPrice
    )
    {
        var erros = new List<string>();

        if (orderId <= 0)
            erros.Add("OrdeId inválido.");
        
        if (productId <= 0)
            erros.Add("productId inválido.");

        if (string.IsNullOrWhiteSpace(productName))
            erros.Add("productName inválido.");

        var resultadoQuantity = Quantity.Create(quantity);

        if (resultadoQuantity.ContemErros)
            erros.AddRange(resultadoQuantity.Erros);
        
        var resultadoUnitPrice = Money.Create(unitPrice);

        if (resultadoUnitPrice.ContemErros)
            erros.AddRange(resultadoUnitPrice.Erros);

        var resultadoTotalPrice = Money.Create(totalPrice);

        if (resultadoTotalPrice.ContemErros)
            erros.AddRange(resultadoTotalPrice.Erros);

        if (erros.Any())
            return Resultado<OrderItem>.Falhar(erros);

        var orderItem = new OrderItem(
            orderId,
            productId,
            productName,
            resultadoQuantity.Valor,
            resultadoUnitPrice.Valor,
            resultadoTotalPrice.Valor
        );

        return Resultado<OrderItem>.Ok(orderItem);
    }
}

using System.Collections.Generic;

namespace Gestao.Pedidos.Core.Entities;

public class Order : BaseEntity
{
    Order(
        int customerId,
        DateTime orderDate, 
        Money totalAmount, 
        OrderStatus status
    )
    {
        CustomerId = customerId;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
        Status = status;
    }

    private  Order(){}

    public int CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public Money TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }

    public virtual Customer Customer { get; private set; }

    public List<OrderItem> Items { get; private set; }

    public static Resultado<Order> Create(
        int customerId,
        List<OrderItem> items
    )
    {
        var erros = new List<string>();

        if (customerId <= 0)
            erros.Add("customerId inválido.");

        if (items is null || !items.Any())
            erros.Add("O pedido deve conter ao menos um item.");

        var totalAmount = items
            .Select(x => x.TotalPrice.Value)
            .Sum();

        var resultadoTotalAmount = Money.Create(totalAmount);

        if (resultadoTotalAmount.ContemErros)
            erros.AddRange(resultadoTotalAmount.Erros);

        if (erros.Any())
            return Resultado<Order>.Falhar(erros);        

        var order = new Order(
            customerId,
            DateTime.UtcNow,
            resultadoTotalAmount.Valor,
            OrderStatus.Confirmed
        );

        return Resultado<Order>.Ok(order);
    }
}
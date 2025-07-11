namespace Gestao.Pedidos.Core.Entities;

public class Order : BaseEntity
{
    Order(
        int customerId,
        DateTime orderDate,
        Money totalAmount,
        OrderStatus status,
        List<OrderItem> items
    )
    {
        CustomerId = customerId;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
        Status = status;

        foreach (var item in items)
            Items.Add(item);
    }

    private Order() { }

    public int CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public Money TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }

    public virtual Customer Customer { get; private set; }

    public virtual ICollection<OrderItem> Items { get; private set; } = new List<OrderItem>();

    public static Resultado<Order> Create(
        int customerId,
        List<OrderItem> items
    )
    {
        var erros = Validar(customerId, items);

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
            OrderStatus.Confirmed,
            items
        );

        return Resultado<Order>.Ok(order);
    }

    public Resultado Update(
          int customerId,
          List<OrderItem> items
      )
    {
        var erros = Validar(customerId, items);

        var totalAmount = items
            .Select(x => x.TotalPrice.Value)
            .Sum();

        var resultadoTotalAmount = Money.Create(totalAmount);

        if (resultadoTotalAmount.ContemErros)
            erros.AddRange(resultadoTotalAmount.Erros);

        if (erros.Any())
            return Resultado.Falhar(erros);

        CustomerId = customerId;
        TotalAmount = resultadoTotalAmount.Valor;
        OrderDate = DateTime.UtcNow;

        Items.Clear();

        foreach (var item in items)
            Items.Add(item);

        return Resultado.Ok();
    }

    public void SetCustomer(Customer customer)
        => Customer = customer;

    static List<string> Validar(int customerId, List<OrderItem> items)
    {
        var erros = new List<string>();

        if (customerId <= 0)
            erros.Add("customerId inválido.");

        if (items is null || !items.Any())
            erros.Add("O pedido deve conter ao menos um item.");

        return erros;
    }
}
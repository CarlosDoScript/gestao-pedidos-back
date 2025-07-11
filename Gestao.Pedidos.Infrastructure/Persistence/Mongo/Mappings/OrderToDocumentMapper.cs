namespace Gestao.Pedidos.Infrastructure.Persistence.Mongo.Mappings;

public static class OrderToDocumentMapper
{
    public static OrderDocument Map(Order order)
    {
        return new OrderDocument
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            CustomerName = order.Customer.Name,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount.Value,
            Status = order.Status.ToString(),
            Items = order.Items.Select(x => new OrderItemDocument
            {
                Id = x.Id,
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Quantity = x.Quantity.Value,
                UnitPrice = x.UnitPrice.Value,
            }).ToList()
        };
    }
}
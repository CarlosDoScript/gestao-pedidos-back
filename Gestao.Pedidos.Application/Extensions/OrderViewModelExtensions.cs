namespace Gestao.Pedidos.Application.Extensions;

public static class OrderViewModelExtensions
{
    public static OrderViewModel ToViewModel(this Order order)
    {
        return new OrderViewModel
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            CustomerName = order.Customer?.Name,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount.Value,
            Status = order.Status.ToString(),
            Items = order.Items.Select(item => new OrderItemViewModel
            {
                Id = item.Id,
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity.Value,
                UnitPrice = item.UnitPrice.Value,
            }).ToList()
        };
    }
}
namespace Gestao.Pedidos.Application.Extensions;

public static class OrderViewModelExtensions
{
    public static OrderViewModel ToViewModel(this Order order)
    {
        return new OrderViewModel
        (
            order.Id,
            order.CustomerId,
            order.Customer?.Name,
            order.OrderDate.ToString("dd/MM/yyyy"),
            order.TotalAmount.Value.ToString("N2"),
            order.Status.ToString(),
            order.Items.Select(item => new OrderItemViewModel
            (
                item.Id,
                item.ProductId,
                item.ProductName,
                item.Quantity.Value,
                item.UnitPrice.Value.ToString("N2")
            )).ToList()
        );
    }

    public static OrderViewModel ToViewModel(this OrderDocument order)
    {
        return new OrderViewModel
        (
            order.Id,
            order.CustomerId,
            order.CustomerName,
            order.OrderDate.ToString("dd/MM/yyyy"),
            order.TotalAmount.ToString("N2"),
            order.Status.ToString(),
            order.Items.Select(item => new OrderItemViewModel
            (
              item.Id,
                item.ProductId,
                item.ProductName,
                item.Quantity,
                item.UnitPrice.ToString("N2")
            )).ToList()
        );
    }
}
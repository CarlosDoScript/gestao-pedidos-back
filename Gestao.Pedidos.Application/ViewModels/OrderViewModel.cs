namespace Gestao.Pedidos.Application.ViewModels;

public record OrderViewModel(
    int Id,
    int CustomerId,
    string CustomerName,
    string OrderDate,
    string TotalAmount,
    string Status,
    List<OrderItemViewModel> Items
);

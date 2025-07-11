namespace Gestao.Pedidos.Application.ViewModels;

public record OrderItemViewModel(
    int Id,
    int ProductId,
    string ProductName,
    int Quantity,
    string UnitPrice
);
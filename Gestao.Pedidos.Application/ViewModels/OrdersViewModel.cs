namespace Gestao.Pedidos.Application.ViewModels;

public record OrdersViewModel(
    int id,
    int customerId,
    string customerName,
    string orderDate,
    string totalAmount,
    string status
);

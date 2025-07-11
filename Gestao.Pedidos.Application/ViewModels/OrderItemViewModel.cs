namespace Gestao.Pedidos.Application.ViewModels;

public record OrderItemViewModel
{
    public int ProductId { get; init; }
    public string ProductName { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal TotalPrice { get; init; }
}

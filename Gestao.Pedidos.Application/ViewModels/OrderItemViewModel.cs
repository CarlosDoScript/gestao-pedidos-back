namespace Gestao.Pedidos.Application.ViewModels;

public record OrderItemViewModel
{
    public int Id { get; init; }
    public int ProductId { get; init; }
    public string ProductName { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
}

namespace Gestao.Pedidos.Application.ViewModels;

public record OrderViewModel
{
    public int Id { get; init; }
    public int CustomerId { get; init; }
    public string CustomerName { get; init; }
    public DateTime OrderDate { get; init; }
    public decimal TotalAmount { get; init; }
    public string Status { get; init; }

    public List<OrderItemViewModel> Items { get; init; } = new();
}

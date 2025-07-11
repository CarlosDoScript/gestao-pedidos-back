namespace Gestao.Pedidos.Core.Extensions;

public static class OrderStatusExtensions
{
    public static string ToDisplayName(this OrderStatus status) => status switch
    {
        OrderStatus.Pending => "Pendente",
        OrderStatus.Confirmed => "Confirmado",
        OrderStatus.Processing => "Processando",
        OrderStatus.Shipped => "Enviado",
        OrderStatus.Delivered => "Entregue",
        OrderStatus.Canceled => "Cancelado",
        _ => "Inválido"
    };
}

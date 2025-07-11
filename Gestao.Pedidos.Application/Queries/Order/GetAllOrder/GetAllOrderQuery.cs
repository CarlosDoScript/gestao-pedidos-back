namespace Gestao.Pedidos.Application.Queries.Order.GetAllOrder;

public  class GetAllOrderQuery : ConsultaPaginada, IRequest<Resultado<Paginacao<OrdersViewModel>>>
{
}
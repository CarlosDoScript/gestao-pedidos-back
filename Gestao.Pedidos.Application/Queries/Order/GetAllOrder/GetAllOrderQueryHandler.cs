namespace Gestao.Pedidos.Application.Queries.Order.GetAllOrder;

public sealed class GetAllOrderQueryHandler(
        IOrderMongoRepository orderMongoRepository
    ) : IRequestHandler<GetAllOrderQuery, Resultado<Paginacao<OrdersViewModel>>>
{
    public async Task<Resultado<Paginacao<OrdersViewModel>>> Handle(GetAllOrderQuery query, CancellationToken cancellationToken)
    {
        var registros = await orderMongoRepository.ObterPedidosPaginadosAsync(query);

        var orders = registros.Itens
            .Select(x => new OrdersViewModel(
                  x.Id,
                  x.CustomerId,
                  x.CustomerName,
                  x.OrderDate.ToShortDateString(),
                  x.TotalAmount.ToString("N2"),
                  x.Status
            )).ToList();

        var paginacao = new Paginacao<OrdersViewModel>(orders, registros.TotalRegistros, registros.PaginaAtual, registros.TotalPaginas);
        return Resultado<Paginacao<OrdersViewModel>>.Ok(paginacao);
    }
}
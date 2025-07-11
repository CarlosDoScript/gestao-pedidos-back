namespace Gestao.Pedidos.Application.Queries.Order.GetAllOrder;

public sealed class GetAllOrderQueryHandler(
        IOrderMongoRepository orderMongoRepository
    ) : IRequestHandler<GetAllOrderQuery, Resultado<Paginacao<OrderDocument>>>
{
    public async Task<Resultado<Paginacao<OrderDocument>>> Handle(GetAllOrderQuery query, CancellationToken cancellationToken)
    {
        var registros = await orderMongoRepository.ObterPedidosPaginadosAsync(query);
        return Resultado<Paginacao<OrderDocument>>.Ok(registros);
    }
}
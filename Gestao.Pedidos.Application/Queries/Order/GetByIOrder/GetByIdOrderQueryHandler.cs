namespace Gestao.Pedidos.Application.Queries.Order.GetByIOrder;

public sealed class GetByIdOrderQueryHandler(
        IOrderMongoRepository orderMongoRepository    
    ) : IRequestHandler<GetByIdOrderQuery, Resultado<OrderDocument>>
{
    public async Task<Resultado<OrderDocument>> Handle(GetByIdOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await orderMongoRepository.GetByIdAsync(request.OrderId);
        return Resultado<OrderDocument>.Ok(order);
    }
}
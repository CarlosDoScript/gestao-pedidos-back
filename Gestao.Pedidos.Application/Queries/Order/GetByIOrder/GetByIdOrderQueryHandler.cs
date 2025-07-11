namespace Gestao.Pedidos.Application.Queries.Order.GetByIOrder;

public sealed class GetByIdOrderQueryHandler(
        IOrderMongoRepository orderMongoRepository    
    ) : IRequestHandler<GetByIdOrderQuery, Resultado<OrderViewModel>>
{
    public async Task<Resultado<OrderViewModel>> Handle(GetByIdOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await orderMongoRepository.GetByIdAsync(request.OrderId);
        return Resultado<OrderViewModel>.Ok(order.ToViewModel());
    }
}
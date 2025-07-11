namespace Gestao.Pedidos.Infrastructure.Persistence.Mongo.Contracts;

public interface IOrderMongoRepository
{
    Task InsertAsync(OrderDocument document);
    Task UpdateAsync(OrderDocument document);
    Task DeleteAsync(int orderId);
    Task<Paginacao<OrderDocument>> ObterPedidosPaginadosAsync(ConsultaPaginada filtro, CancellationToken cancellationToken = default);
}
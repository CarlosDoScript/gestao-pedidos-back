namespace Gestao.Pedidos.Infrastructure.Persistence.Mongo.Contracts;

public interface IOrderMongoRepository
{
    Task<OrderDocument> GetByIdAsync(int id);
    Task<Paginacao<OrdersDocument>> ObterPedidosPaginadosAsync(ConsultaPaginada filtro, CancellationToken cancellationToken = default);
    Task InsertAsync(OrderDocument document);
    Task UpdateAsync(OrderDocument document);
    Task DeleteAsync(int orderId);
}
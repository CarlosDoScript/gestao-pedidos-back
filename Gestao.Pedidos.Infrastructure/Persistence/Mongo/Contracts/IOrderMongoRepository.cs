namespace Gestao.Pedidos.Infrastructure.Persistence.Mongo.Contracts;

public interface IOrderMongoRepository
{
    Task InsertAsync(OrderDocument document);
    Task UpdateAsync(OrderDocument document);
}
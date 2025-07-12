namespace Gestao.Pedidos.Infrastructure.Persistence.Mongo.Contracts;

public interface IProductMongoRepository
{
    Task<IEnumerable<ProductDocument>> GetAllAsync();
}

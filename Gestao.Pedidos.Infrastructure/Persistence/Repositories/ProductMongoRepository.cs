namespace Gestao.Pedidos.Infrastructure.Persistence.Repositories;

public class ProductMongoRepository(
        IMongoCollection<ProductDocument> collection
    ) : IProductMongoRepository
{
    public async Task<IEnumerable<ProductDocument>> GetAllAsync()
    {
        return await collection
       .Find(FilterDefinition<ProductDocument>.Empty)
       .ToListAsync();
    }
}

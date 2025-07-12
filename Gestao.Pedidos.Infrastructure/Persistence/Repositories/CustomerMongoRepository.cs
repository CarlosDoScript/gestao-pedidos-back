namespace Gestao.Pedidos.Infrastructure.Persistence.Repositories;

public class CustomerMongoRepository(
        IMongoCollection<CustomerDocument> collection
    ) : ICustomerMongoRepository
{
    public async Task<CustomerDocument?> GetByIdAsync(int id)
    {
        var filter = Builders<CustomerDocument>.Filter.Eq(x => x.Id, id);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<CustomerDocument>> GetAllAsync()
    {
        return await collection
         .Find(FilterDefinition<CustomerDocument>.Empty)
         .ToListAsync();
    }

    public async Task InsertAsync(CustomerDocument document)
    {
        await collection.InsertOneAsync(document);
    }
}
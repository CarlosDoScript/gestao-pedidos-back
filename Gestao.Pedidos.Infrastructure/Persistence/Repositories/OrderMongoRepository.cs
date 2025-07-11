namespace Gestao.Pedidos.Infrastructure.Persistence.Repositories;

public class OrderMongoRepository : IOrderMongoRepository
{
    private readonly IMongoCollection<OrderDocument> _collection;

    public OrderMongoRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<OrderDocument>("orders");
    }

    public async Task InsertAsync(OrderDocument document)
    {
        await _collection.InsertOneAsync(document);
    }

    public async Task UpdateAsync(OrderDocument document)
    {
        var filter = Builders<OrderDocument>.Filter.Eq(x => x.Id, document.Id);
        var options = new ReplaceOptions { IsUpsert = true };

        await _collection.ReplaceOneAsync(filter, document, options);
    }
}
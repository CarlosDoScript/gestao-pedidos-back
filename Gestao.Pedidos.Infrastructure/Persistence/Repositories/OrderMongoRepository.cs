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
}

namespace Gestao.Pedidos.Infrastructure.Persistence.Repositories;

public class OrderMongoRepository(
        IMongoCollection<OrderDocument> collection
    ) : IOrderMongoRepository
{
    public async Task InsertAsync(OrderDocument document)
    {
        await collection.InsertOneAsync(document);
    }

    public async Task UpdateAsync(OrderDocument document)
    {
        var filter = Builders<OrderDocument>.Filter.Eq(x => x.Id, document.Id);
        var options = new ReplaceOptions { IsUpsert = true };

        await collection.ReplaceOneAsync(filter, document, options);
    }

    public async Task DeleteAsync(int orderId)
    {
        var filter = Builders<OrderDocument>.Filter.Eq(x => x.Id, orderId);
        await collection.DeleteOneAsync(filter);
    }

    public async Task<Paginacao<OrderDocument>> ObterPedidosPaginadosAsync(ConsultaPaginada filtro, CancellationToken cancellationToken = default)
    {
        var sort = filtro.OrdemAscendente
        ? Builders<OrderDocument>.Sort.Ascending(filtro.OrdenarPor ?? "_id")
        : Builders<OrderDocument>.Sort.Descending(filtro.OrdenarPor ?? "_id");

        var totalRegistros = await collection
            .CountDocumentsAsync(FilterDefinition<OrderDocument>.Empty, cancellationToken: cancellationToken);

        var items = await collection
            .Find(FilterDefinition<OrderDocument>.Empty)
            .Sort(sort)
            .Skip((filtro.NumeroPagina - 1) * filtro.TamanhoPagina)
            .Limit(filtro.TamanhoPagina)
            .ToListAsync();

        return new Paginacao<OrderDocument>(items, ((int)totalRegistros), filtro.NumeroPagina, filtro.TamanhoPagina);
    }
}
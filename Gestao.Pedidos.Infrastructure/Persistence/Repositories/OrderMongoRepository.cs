namespace Gestao.Pedidos.Infrastructure.Persistence.Repositories;

public class OrderMongoRepository(
        IMongoCollection<OrderDocument> collection
    ) : IOrderMongoRepository
{
    public async Task<Paginacao<OrdersDocument>> ObterPedidosPaginadosAsync(ConsultaPaginada filtro, CancellationToken cancellationToken = default)
    {
        var totalRegistros = await collection.CountDocumentsAsync(
            FilterDefinition<OrderDocument>.Empty,
            cancellationToken: cancellationToken);

        var totalPaginas = totalRegistros <= 0
            ? 0
            : (int)Math.Ceiling(totalRegistros / (double)filtro.TamanhoPagina);

        var pagina = filtro.NumeroPagina;

        if (pagina < 1)
            pagina = 1;

        if (pagina > totalPaginas)
            pagina = totalPaginas == 0 ? 1 : totalPaginas;

        var sort = filtro.OrdemAscendente
            ? Builders<OrderDocument>.Sort.Ascending(filtro.OrdenarPor ?? "_id")
            : Builders<OrderDocument>.Sort.Descending(filtro.OrdenarPor ?? "_id");

        var items = await collection
            .Find(FilterDefinition<OrderDocument>.Empty)
            .Sort(sort)
            .Skip((pagina - 1) * filtro.TamanhoPagina)
            .Limit(filtro.TamanhoPagina)
            .ToListAsync(cancellationToken);

        var orders = items.Select(x => new OrdersDocument
        {
            Id = x.Id,
            CustomerId = x.CustomerId,
            CustomerName = x.CustomerName,
            OrderDate = x.OrderDate,
            TotalAmount = x.TotalAmount,
            Status = x.Status
        });

        return new Paginacao<OrdersDocument>(orders, (int)totalRegistros, pagina, filtro.TamanhoPagina);
    }

    public async Task<OrderDocument> GetByIdAsync(int id)
    {
        var filter = Builders<OrderDocument>.Filter.Eq(x => x.Id, id);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

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
}
namespace Gestao.Pedidos.Infrastructure.Persistence.Mongo.Documents;

public class ProductDocument
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

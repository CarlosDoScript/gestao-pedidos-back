namespace Gestao.Pedidos.Infrastructure.Persistence.Mongo.Documents;

public class CustomerDocument
{
    [BsonId]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}

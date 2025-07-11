namespace Gestao.Pedidos.Infrastructure.Persistence.Mongo.Documents;

public class OrderDocument
{
    [BsonId]
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
    public List<OrderItemDocument> Items { get; set; }
}
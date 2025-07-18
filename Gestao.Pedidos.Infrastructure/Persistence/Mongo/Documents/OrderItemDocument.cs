﻿namespace Gestao.Pedidos.Infrastructure.Persistence.Mongo.Documents;

public class OrderItemDocument
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }    
}

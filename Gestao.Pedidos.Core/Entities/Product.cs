namespace Gestao.Pedidos.Core.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; }
    public Money Price { get; private set; }
}

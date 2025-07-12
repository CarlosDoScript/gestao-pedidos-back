namespace Gestao.Pedidos.Core.Entities;

public class Product : BaseEntity
{
    Product(string name, Money price)
    {
        Name = name;
        Price = price;
    }

    private Product() {}

    public string Name { get; private set; }
    public Money Price { get; private set; }

    public static Resultado<Product> Create(string name, decimal price)
    {
        var erros = new List<string>();

        if (string.IsNullOrWhiteSpace(name))
            erros.Add("Nome é obrigatório.");

        var resultadoPrice = Money.Create(price);

        if (resultadoPrice.ContemErros)
            erros.AddRange(resultadoPrice.Erros);

        if (erros.Any())
            return Resultado<Product>.Falhar(erros);

        var product = new Product(name,resultadoPrice.Valor);

        return Resultado<Product>.Ok(product);
    }
}

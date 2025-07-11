namespace Gestao.Pedidos.Core.ValueObjects;

public class Quantity : ValueObject
{
    public int Value { get; init; }

    private Quantity(int value) => Value = value;

    public static Resultado<Quantity> Create(int value)
    {
        if (value <= 0)
            return Resultado<Quantity>.Falhar("Quantidade deve ser maior que 0");

        return Resultado<Quantity>.Ok(new Quantity(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
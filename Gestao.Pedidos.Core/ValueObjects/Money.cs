namespace Gestao.Pedidos.Core.ValueObjects;

public class Money : ValueObject
{
    public decimal Value { get; init; }

    private Money(decimal value) => Value = value;

    public static Resultado<Money> Create(decimal value)
    {
        if (value < 0)
            return Resultado<Money>.Falhar("Valor não pode ser negativo");

        return Resultado<Money>.Ok(new Money(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Money operator +(Money a, Money b) => new(a.Value + b.Value);
    public static Money operator *(Money a, int quantidade) => new(a.Value * quantidade);
}
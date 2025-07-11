namespace Gestao.Pedidos.Core.ValueObjects;

public class Phone : ValueObject
{
    public string Number { get; init; }

    private Phone(string number) => Number = number;

    public static Resultado<Phone> Create(string number)
    {
        if (string.IsNullOrWhiteSpace(number) || number.Length < 10)
            return Resultado<Phone>.Falhar("Telefone inválido");

        return Resultado<Phone>.Ok(new Phone(number));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
    }
}
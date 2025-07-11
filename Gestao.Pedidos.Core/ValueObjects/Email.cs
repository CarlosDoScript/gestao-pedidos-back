namespace Gestao.Pedidos.Core.ValueObjects;

public class Email : ValueObject
{
    public string Address { get; init; }

    private Email(string address) => Address = address;

    public static Resultado<Email> Create(string address)
    {
        if (string.IsNullOrWhiteSpace(address) || !Regex.IsMatch(address, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return Resultado<Email>.Falhar("E-mail inválido");

        return Resultado<Email>.Ok(new Email(address));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address.ToLowerInvariant();
    }
}

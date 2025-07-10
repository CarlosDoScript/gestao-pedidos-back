namespace Gestao.Pedidos.Core.Entities;

public class Customer : BaseEntity
{
    Customer(
        string name, 
        Email email,
        Phone phone
    )
    {
        Name = name;
        Email = email;
        Phone = phone;
    }

    public string Name { get; private set; }
    public Email Email { get; private set; }
    public Phone Phone { get; private set; }

    public static Resultado<Customer> Criar(
        string name,
        string email,
        string phone
    )
    {
        var erros = new List<string>();

        var resultadoEmail = Email.Criar(email);

        if (resultadoEmail.ContemErros)
            erros.AddRange(resultadoEmail.Erros);

        var resultadoPhone = Phone.Criar(phone);

        if (resultadoPhone.ContemErros)
            erros.AddRange(resultadoPhone.Erros);

        if (string.IsNullOrWhiteSpace(name))
            erros.Add("Nome é obrigatório.");

        if (erros.Any())
            return Resultado<Customer>.Falhar(erros);

        var customer = new Customer(name,resultadoEmail.Valor,resultadoPhone.Valor);

        return Resultado<Customer>.Ok(customer);
    }
}
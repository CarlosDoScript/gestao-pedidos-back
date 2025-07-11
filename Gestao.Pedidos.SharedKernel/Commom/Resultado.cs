namespace Gestao.Pedidos.SharedKernel.Commom;

public sealed class Resultado<T>
{
    public bool Sucesso => !Erros.Any() && !_forcarFalha;
    public bool ContemErros => Erros.Any() && _forcarFalha;
    public IReadOnlyCollection<string> Erros { get; }
    public T Valor { get; }

    readonly bool _forcarFalha;

    private Resultado(T valor, List<string> erros, bool forcarFalha = false)
    {
        Valor = valor;
        Erros = erros;
        _forcarFalha = forcarFalha;
    }

    public static Resultado<T> Ok(T valor) => new(valor, new());
    public static Resultado<T> Falhar(params string[] erros) => new(default, erros.ToList());
    public static Resultado<T> Falhar(IEnumerable<string> erros) => new(default, erros.ToList());
    public static Resultado<T> Falhar(T errorObject) => new(errorObject, new(), forcarFalha: true);
}

public sealed class Resultado
{
    public bool Sucesso => !Erros.Any();
    public bool ContemErros => Erros.Any();
    public IReadOnlyCollection<string> Erros { get; }

    private Resultado(List<string> erros)
    {
        Erros = erros;
    }

    public static Resultado Ok() => new(new());
    public static Resultado Falhar(params string[] erros) => new(erros.ToList());
    public static Resultado Falhar(IEnumerable<string> erros) => new(erros.ToList());
}
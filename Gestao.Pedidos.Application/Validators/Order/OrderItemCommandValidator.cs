namespace Gestao.Pedidos.Application.Validators.Order;

public class OrderItemCommandValidator : AbstractValidator<OrderItemInputModel>
{
    public OrderItemCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0)
            .WithMessage("Produto é obrigatório.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantidade deve ser maior que zero.");
    }
}
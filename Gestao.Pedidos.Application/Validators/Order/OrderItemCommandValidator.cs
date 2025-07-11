namespace Gestao.Pedidos.Application.Validators.Order;

public class OrderItemCommandValidator : AbstractValidator<OrderItemInputModel>
{
    public OrderItemCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ProductId deve ser maior que zero.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity deve ser maior que zero.");
    }
}
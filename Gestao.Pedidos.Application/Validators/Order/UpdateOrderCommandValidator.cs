namespace Gestao.Pedidos.Application.Validators.Order;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
        .GreaterThan(0)
        .WithMessage("Customer é obrigatório.");

        RuleFor(x => x.Items)
            .NotNull().WithMessage("Items não pode ser nulo.")
            .NotEmpty().WithMessage("O pedido deve conter ao menos um item.");

        RuleForEach(x => x.Items).SetValidator(new OrderItemCommandValidator());
    }
}

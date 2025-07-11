namespace Gestao.Pedidos.Application.Validators.Order;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
         .GreaterThan(0).WithMessage("CustomerId deve ser maior que zero.");

        RuleFor(x => x.Items)
            .NotNull().WithMessage("Items não pode ser nulo.")
            .NotEmpty().WithMessage("O pedido deve conter ao menos um item.");

        RuleForEach(x => x.Items).SetValidator(new OrderItemCommandValidator());
    }
}

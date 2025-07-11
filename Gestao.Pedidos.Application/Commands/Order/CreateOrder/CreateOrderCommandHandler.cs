namespace Gestao.Pedidos.Application.Commands.Order.CreateOrder;

public class CreateOrderCommandHandler(
        
    ) : IRequestHandler<CreateOrderCommand, Resultado<OrderViewModel>>
{
    public async Task<Resultado<OrderViewModel>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        //var customer = await _customerRepository.GetByIdAsync(command.CustomerId);

        //if (customer is null)
        //    return Resultado<Order>.Falhar("Cliente não encontrado.");

        //foreach (var item in command.Items)
        //{
        //    var product = await 
        //}

        return Resultado<OrderViewModel>.Ok(new OrderViewModel());
    }
}

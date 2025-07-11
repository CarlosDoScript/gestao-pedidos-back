namespace Gestao.Pedidos.Application.Commands.Order.DeleteOrder;

public sealed class DeleteOrderCommandHandler(
        IOrderRepository orderRepository,
        IOrderMongoRepository orderMongoRepository
    ) : IRequestHandler<DeleteOrderCommand, Resultado>
{
    public async Task<Resultado> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(command.OrderId);

        if (order is null)
            return Resultado.Falhar("Pedido não encontrado.");

        await orderRepository.RemoveAsync(order);
        await orderRepository.SaveAsync();

        await orderMongoRepository.DeleteAsync(command.OrderId);

        return Resultado.Ok();
    }
}
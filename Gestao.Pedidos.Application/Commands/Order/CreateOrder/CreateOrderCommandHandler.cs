namespace Gestao.Pedidos.Application.Commands.Order.CreateOrder;

public sealed class CreateOrderCommandHandler(
        ICustomerRepository customerRepository,
        IProductRepository productRepository,
        IOrderRepository orderRepository,
        IOrderMongoRepository orderMongoRepository
    ) : IRequestHandler<CreateOrderCommand, Resultado<OrderViewModel>>
{
    public async Task<Resultado<OrderViewModel>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(command.CustomerId);

        if (customer is null)
            return Resultado<OrderViewModel>.Falhar("Cliente não encontrado.");

        var orderItems = new List<OrderItem>();

        foreach (var item in command.Items)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId);

            if (product is null)
                return Resultado<OrderViewModel>.Falhar($"Produto {item.ProductId} não encontrado.");
            
            var resultadoOrderItem = OrderItem.Create(
                item.ProductId,
                product.Name,
                item.Quantity,
                product.Price.Value
            );

            if (resultadoOrderItem.ContemErros)
                return Resultado<OrderViewModel>.Falhar(resultadoOrderItem.Erros);

            orderItems.Add(resultadoOrderItem.Valor);
        }

        var resultadoOrder = Core.Entities.Order.Create(
            command.CustomerId,
            orderItems
        );

        if (resultadoOrder.ContemErros)
            return Resultado<OrderViewModel>.Falhar(resultadoOrder.Erros);

        await orderRepository.AddAsync(resultadoOrder.Valor);
        await orderRepository.SaveAsync();

        resultadoOrder.Valor.SetCustomer(customer);

        var orderDocumento = OrderToDocumentMapper.Map(resultadoOrder.Valor);

        await orderMongoRepository.InsertAsync(orderDocumento);

        return Resultado<OrderViewModel>.Ok(resultadoOrder.Valor.ToViewModel());
    }
}
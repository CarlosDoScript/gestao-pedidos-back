namespace Gestao.Pedidos.Application.Commands.Order.UpdateOrder;

public sealed class UpdateOrderCommandHandler(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IProductRepository productRepository,
        IOrderMongoRepository orderMongoRepository,
        IOrderItemRepository orderItemRepository,
        GestaoPedidosDbContext context
    ) : IRequestHandler<UpdateOrderCommand, Resultado<OrderViewModel>>
{
    public async Task<Resultado<OrderViewModel>> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var order = await orderRepository.GetByIdAndItemsAsync(command.OrderId);

            if (order is null)
                return Resultado<OrderViewModel>.Falhar("Pedido não encontrado.");

            var resultadoItems = await ValidarEObterOrderItemsAsync(command.Items);

            if (resultadoItems.ContemErros)
                return Resultado<OrderViewModel>.Falhar(resultadoItems.Erros);

            await orderItemRepository.RemoveAllAsync(order.Items);

            var resultadoOrderUpdate = order.Update(command.CustomerId, resultadoItems.Valor);

            if (resultadoOrderUpdate.ContemErros)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Resultado<OrderViewModel>.Falhar(resultadoOrderUpdate.Erros);
            }

            var customer = await customerRepository.GetByIdAsync(command.CustomerId);

            if (customer is null)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Resultado<OrderViewModel>.Falhar("Cliente não encontrado.");
            }

            await orderRepository.UpdateAsync(order);
            await orderRepository.SaveAsync();
            await transaction.CommitAsync(cancellationToken);

            order.SetCustomer(customer);

            var document = OrderToDocumentMapper.Map(order);
            await orderMongoRepository.UpdateAsync(document);

            return Resultado<OrderViewModel>.Ok(order.ToViewModel());
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    async Task<Resultado<List<OrderItem>>> ValidarEObterOrderItemsAsync(List<OrderItemInputModel> items)
    {
        var resultado = new List<OrderItem>();
        var erros = new List<string>();

        foreach (var item in items)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId);

            if (product is null)
            {
                erros.Add($"Produto {item.ProductId} não encontrado.");
                continue;
            }

            var resultadoItem = OrderItem.Create(
                item.ProductId,
                product.Name,
                item.Quantity,
                product.Price.Value
            );

            if (resultadoItem.ContemErros)
                erros.AddRange(resultadoItem.Erros);
            else
                resultado.Add(resultadoItem.Valor);
        }

        return erros.Any()
            ? Resultado<List<OrderItem>>.Falhar(erros)
            : Resultado<List<OrderItem>>.Ok(resultado);
    }
}

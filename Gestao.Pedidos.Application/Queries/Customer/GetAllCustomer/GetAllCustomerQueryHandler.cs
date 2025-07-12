namespace Gestao.Pedidos.Application.Queries.Customer.GetAllCustomer;

public sealed class GetAllCustomerQueryHandler(
       ICustomerMongoRepository customerMongoRepository
    ) : IRequestHandler<GetAllCustomerQuery, Resultado<IEnumerable<CustomerViewModel>>>
{
    public async Task<Resultado<IEnumerable<CustomerViewModel>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        var customersDocumento = await customerMongoRepository.GetAllAsync();

        var customersViewModel = customersDocumento.Select(x => new CustomerViewModel(
            x.Id,
            x.Name
        )).ToList();

        return Resultado<IEnumerable<CustomerViewModel>>.Ok(customersViewModel);
    }
}

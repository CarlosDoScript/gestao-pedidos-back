namespace Gestao.Pedidos.Infrastructure.Persistence.Mongo.Contracts;

public interface ICustomerMongoRepository
{    
    Task<IEnumerable<CustomerDocument>> GetAllAsync();    
}
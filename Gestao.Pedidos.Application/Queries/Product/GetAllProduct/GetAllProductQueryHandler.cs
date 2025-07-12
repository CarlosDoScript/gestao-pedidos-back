
namespace Gestao.Pedidos.Application.Queries.Product.GetAllProduct;

public sealed class GetAllProductQueryHandler(
        IProductMongoRepository productMongoRepository
    ) : IRequestHandler<GetAllProductQuery, Resultado<IEnumerable<ProductViewModel>>>
{
    public async Task<Resultado<IEnumerable<ProductViewModel>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var productsDocument = await productMongoRepository.GetAllAsync();

        var productsViewModel = productsDocument.Select(x => new ProductViewModel(x.Id,x.Name)).ToList();

        return Resultado<IEnumerable<ProductViewModel>>.Ok(productsViewModel);
    }
}
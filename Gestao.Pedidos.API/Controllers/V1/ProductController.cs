using Gestao.Pedidos.Application.Queries.Product.GetAllProduct;
using Gestao.Pedidos.Application.ViewModels;
using MediatR;

namespace Gestao.Pedidos.API.Controllers.V1;

[Route("api/v1/product")]
[Tags("Product")]
public class ProductController(
        IMediator mediator
    ) : ApiV1Controller
{
    /// <summary>
    /// Retorna todos os produtos.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(Resultado<IEnumerable<ProductViewModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Resultado), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetAllProductQuery();
        var resultado = await mediator.Send(query);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }
}

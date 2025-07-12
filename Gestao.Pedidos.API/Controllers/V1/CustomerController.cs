using Gestao.Pedidos.Application.Queries.Customer.GetAllCustomer;
using Gestao.Pedidos.Application.ViewModels;
using MediatR;

namespace Gestao.Pedidos.API.Controllers.V1;

[Route("api/v1/customer")]
[Tags("Customer")]
public class CustomerController(
        IMediator mediator
    ) : ApiV1Controller
{
    /// <summary>
    /// Retorna todos os clientes.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(Resultado<IEnumerable<CustomerViewModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Resultado), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetAllCustomerQuery();
        var resultado = await mediator.Send(query);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }
}

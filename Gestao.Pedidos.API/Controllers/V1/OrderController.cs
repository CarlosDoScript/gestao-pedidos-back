using Gestao.Pedidos.Application.Commands.Order.CreateOrder;
using MediatR;

namespace Gestao.Pedidos.API.Controllers.V1;

[Route("api/v1/order")]
public class OrderController(
        IMediator mediator
    ) : ApiV1Controller
{
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateOrderCommand command)
    {
        var resultado = await mediator.Send(command);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }
}

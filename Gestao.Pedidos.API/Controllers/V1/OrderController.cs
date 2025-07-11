using Gestao.Pedidos.Application.Commands.Order.CreateOrder;
using Gestao.Pedidos.Application.Commands.Order.DeleteOrder;
using Gestao.Pedidos.Application.Commands.Order.UpdateOrder;
using Gestao.Pedidos.Application.Queries.Order.GetAllOrder;
using Gestao.Pedidos.Application.Queries.Order.GetByIOrder;
using MediatR;

namespace Gestao.Pedidos.API.Controllers.V1;

[Route("api/v1/order")]
public class OrderController(
        IMediator mediator
    ) : ApiV1Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetAllOrderQuery query)
    {
        var resultado = await mediator.Send(query);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }

    [HttpGet("{id}")]  
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var query = new GetByIdOrderQuery() { OrderId = id };

        var resultado = await mediator.Send(query);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateOrderCommand command)
    {
        var resultado = await mediator.Send(command);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateOrderCommand command)
    {
        command.SetId(id);

        var resultado = await mediator.Send(command);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var command = new DeleteOrderCommand(id);

        var resultado = await mediator.Send(command);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }
}

using Gestao.Pedidos.Application.Commands.Order.CreateOrder;
using Gestao.Pedidos.Application.Commands.Order.DeleteOrder;
using Gestao.Pedidos.Application.Commands.Order.UpdateOrder;
using Gestao.Pedidos.Application.Queries.Order.GetAllOrder;
using Gestao.Pedidos.Application.Queries.Order.GetByIOrder;
using Gestao.Pedidos.Application.ViewModels;
using MediatR;

namespace Gestao.Pedidos.API.Controllers.V1;

[Route("api/v1/order")]
[Tags("Order")]
public class OrderController(
        IMediator mediator
    ) : ApiV1Controller
{
    /// <summary>
    /// Retorna todos os pedidos.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(Resultado<IEnumerable<OrderViewModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Resultado), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetAllOrderQuery query)
    {
        var resultado = await mediator.Send(query);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }

    /// <summary>
    /// Retorna um pedido específico pelo ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Resultado<OrderViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Resultado), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var query = new GetByIdOrderQuery() { OrderId = id };

        var resultado = await mediator.Send(query);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }

    /// <summary>
    /// Cria um novo pedido.
    /// </summary>
    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(Resultado<OrderViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Resultado), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync([FromBody] CreateOrderCommand command)
    {
        var resultado = await mediator.Send(command);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }

    /// <summary>
    /// Atualiza um pedido existente.
    /// </summary>
    [HttpPut("{id}")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(Resultado<OrderViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Resultado), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateOrderCommand command)
    {
        command.SetId(id);

        var resultado = await mediator.Send(command);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }

    /// <summary>
    /// Remove um pedido existente.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Resultado), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Resultado), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var command = new DeleteOrderCommand(id);

        var resultado = await mediator.Send(command);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }
}

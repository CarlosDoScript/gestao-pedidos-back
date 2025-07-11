using System.Net;
using System.Text.Json;

namespace Gestao.Pedidos.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IAppLogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, IAppLogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = Resultado<dynamic>.Falhar(new
            {
                Status = context.Response.StatusCode,
                Message = ex.Message,
                StackTrace = ex.StackTrace
            });

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
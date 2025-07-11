using Microsoft.OpenApi.Models;

namespace Gestao.Pedidos.API.Extensions;

public static class SwaggerExtensions
{
    public static void AdicionarSwaggerDocV1(this IServiceCollection services)
    {
        services
        .AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Gestão.Pedidos.API",
                Version = "v1"
            });
            c.IgnoreObsoleteActions();
            c.CustomSchemaIds(type => type.FullName);
        });
    }
}
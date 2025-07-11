using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

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
                Title = "Gestão de Pedidos API",
                Version = "v1"
            });
            c.IgnoreObsoleteActions();
            c.CustomSchemaIds(type => type.FullName);
            
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
    }
}
using Gestao.Pedidos.Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gestao.Pedidos.CrossCutting;

public static class DependencyInjection
{
    public static IServiceCollection ResolveDependencias(this IServiceCollection services, IConfiguration configuration)
    {
        Infraestrutura(services, configuration);
        return services;
    }

    private static void Infraestrutura(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(typeof(IAppLogger<>), typeof(AppLogger<>));
    }
}
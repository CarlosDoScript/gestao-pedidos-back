using Gestao.Pedidos.Application.Commands.Order.CreateOrder;
using Gestao.Pedidos.Core.Repositories;
using Gestao.Pedidos.Infrastructure.Logging;
using Gestao.Pedidos.Infrastructure.Persistence;
using Gestao.Pedidos.Infrastructure.Persistence.Mongo.Contracts;
using Gestao.Pedidos.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Gestao.Pedidos.CrossCutting;

public static class DependencyInjection
{
    public static IServiceCollection ResolveDependencias(this IServiceCollection services, IConfiguration configuration)
    {
        Infraestrutura(services, configuration);
        return services;
    }

    static void Infraestrutura(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(typeof(IAppLogger<>), typeof(AppLogger<>));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));
        AddRepositories(services);
        ResolveConexaoBanco(services, configuration);
    }

    static void AddRepositories(IServiceCollection services)
    {
        services.AddTransient(typeof(IBaseEntityRepository<,>), typeof(BaseEntityRepository<,>));
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderMongoRepository, OrderMongoRepository>();
    }

    static void ResolveConexaoBanco(IServiceCollection services, IConfiguration configuration)
    {
        SqlServer(services, configuration);
        Mongo(services, configuration);
    }

    static void Mongo(IServiceCollection services, IConfiguration configuration)
    {
        var mongoConnectionString = configuration.GetConnectionString("Mongo");
        var mongoCliente = new MongoClient(mongoConnectionString);
        var mongoDatabase = mongoCliente.GetDatabase("GestaoPedidos");

        services.AddSingleton<IMongoClient>(mongoCliente);
        services.AddSingleton(mongoDatabase);
    }

    static void SqlServer(IServiceCollection services, IConfiguration configuration)
    {
        var sqlConnectionString = configuration.GetConnectionString("Sql");
        services.AddDbContext<GestaoPedidosDbContext>(options =>
        options.UseSqlServer(sqlConnectionString));
    }
}
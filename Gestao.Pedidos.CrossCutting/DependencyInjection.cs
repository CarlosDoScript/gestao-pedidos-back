using Gestao.Pedidos.Application.Commands.Order.CreateOrder;
using Gestao.Pedidos.Core.Repositories;
using Gestao.Pedidos.Infrastructure.Logging;
using Gestao.Pedidos.Infrastructure.Persistence;
using Gestao.Pedidos.Infrastructure.Persistence.Mongo.Contracts;
using Gestao.Pedidos.Infrastructure.Persistence.Mongo.Documents;
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
        ResolveConexaoBanco(services, configuration);
        AddRepositories(services);
    }

    static void AddRepositories(IServiceCollection services)
    {
        services.AddTransient(typeof(IBaseEntityRepository<,>), typeof(BaseEntityRepository<,>));
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        services.AddTransient<IOrderItemRepository, OrderItemRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();

        services.AddScoped<IOrderMongoRepository, OrderMongoRepository>();
        services.AddScoped<ICustomerMongoRepository, CustomerMongoRepository>();
        services.AddScoped<IProductMongoRepository, ProductMongoRepository>();

        MongoCollections(services);
    }

    static void MongoCollections(IServiceCollection services)
    {
        services.AddScoped(sp =>
        {
            var db = sp.GetRequiredService<IMongoDatabase>();
            return db.GetCollection<OrderDocument>("orders");
        });
        
        services.AddScoped(sp =>
        {
            var db = sp.GetRequiredService<IMongoDatabase>();
            return db.GetCollection<CustomerDocument>("customers");
        });
        
        services.AddScoped(sp =>
        {
            var db = sp.GetRequiredService<IMongoDatabase>();
            return db.GetCollection<ProductDocument>("products");
        });
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
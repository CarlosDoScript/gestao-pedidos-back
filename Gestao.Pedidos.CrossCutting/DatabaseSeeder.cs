using Gestao.Pedidos.Core.Entities;
using Gestao.Pedidos.Infrastructure.Persistence;
using Gestao.Pedidos.Infrastructure.Persistence.Mongo.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Gestao.Pedidos.CrossCutting;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var customerName = "André Caio da Paz";
        var customerEmail = "andre.caio.dapaz@novaface.com.br";
        var customerPhone = "(38) 99560-9927";
        var context = scope.ServiceProvider.GetRequiredService<GestaoPedidosDbContext>();

        if (!await context.Customer.AnyAsync())
        {
            context.Customer.Add(Customer.Create(customerName, customerEmail, customerPhone).Valor);
        }

        if (!await context.Product.AnyAsync())
        {
            context.Product.AddRange(
                Product.Create("Teclado", 10M).Valor,
                Product.Create("Mouse", 20M).Valor,
                Product.Create("Monitor 25 LED", 30M).Valor
            );
        }

        await context.SaveChangesAsync();

        var mongoCustomerCollection = scope.ServiceProvider.GetRequiredService<IMongoCollection<CustomerDocument>>();
        var mongoProductCollection = scope.ServiceProvider.GetRequiredService<IMongoCollection<ProductDocument>>();

        var existingCustomer = await mongoCustomerCollection.Find(_ => true).FirstOrDefaultAsync();

        if (existingCustomer is null)
        {
            await mongoCustomerCollection.InsertOneAsync(new CustomerDocument
            {
                Id = context.Customer.FirstOrDefault().Id,
                Name = customerName,
                Email = customerEmail,
                Phone = customerPhone,
            });
        }

        var productCount = await mongoProductCollection.CountDocumentsAsync(_ => true);

        if (productCount == 0)
        {
            var products = context.Product.ToList();

            foreach (var product in products)
            {
                var productDocument = new ProductDocument
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price.Value
                };

                await mongoProductCollection.InsertOneAsync(productDocument);
            }
        }
    }
}

using Gestao.Pedidos.API.Extensions;
using Gestao.Pedidos.API.Filters;
using Gestao.Pedidos.API.Middlewares;
using Gestao.Pedidos.Application.Commands.Order.CreateOrder;
using Gestao.Pedidos.CrossCutting;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(options => options.Filters.Add(typeof(FluentValidationMensagensFilter)))
       .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateOrderCommand>());

builder.Services.ResolveDependencias(builder.Configuration);
builder.Services.AdicionarSwaggerDocV1();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();
app.Run();
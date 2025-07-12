using Gestao.Pedidos.API.Extensions;
using Gestao.Pedidos.API.Filters;
using Gestao.Pedidos.API.Middlewares;
using Gestao.Pedidos.Application.Commands.Order.CreateOrder;
using Gestao.Pedidos.CrossCutting;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

builder.WebHost.ConfigureKestrel(options =>
{
    if (isDocker)
    {
        options.ListenAnyIP(80); 
    }
    else
    {
        options.ListenAnyIP(5000); 
        options.ListenAnyIP(5001, listenOptions => listenOptions.UseHttps());
    }
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(options => options.Filters.Add(typeof(FluentValidationMensagensFilter)))
       .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateOrderCommand>());

builder.Services.ResolveDependencias(builder.Configuration);
builder.Services.AdicionarSwaggerDocV1();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    await DatabaseSeeder.SeedAsync(serviceProvider);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();
app.Run();
namespace Gestao.Pedidos.Infrastructure.Persistence;

public partial class GestaoPedidosDbContext : DbContext
{
    public GestaoPedidosDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

public partial class GestaoPedidosDbContext
{
    public DbSet<Customer> Customer => Set<Customer>();
    public DbSet<Order> Order => Set<Order>();
    public DbSet<OrderItem> OrderItem => Set<OrderItem>();
    public DbSet<Product> Product => Set<Product>();
}
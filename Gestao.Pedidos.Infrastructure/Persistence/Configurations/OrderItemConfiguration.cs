namespace Gestao.Pedidos.Infrastructure.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder
            .ToTable(nameof(OrderItem));

        builder
            .HasKey("Id");

        builder
            .HasIndex(x => x.OrderId);

        builder
            .HasIndex(x => x.ProductId);

        builder
            .Property(x => x.ProductName)
            .HasColumnName("ProductName")
            .HasColumnType("VARCHAR")
            .HasMaxLength(250)
            .IsRequired();

        builder.OwnsOne(i => i.UnitPrice, b =>
        {
            b.Property(p => p.Value)
            .HasColumnName("UnitPrice")
            .HasColumnType("DECIMAL(18,2)")
            .IsRequired();
        });

        builder.OwnsOne(i => i.Quantity, b =>
        {
            b.Property(q => q.Value)
            .HasColumnName("Quantity")
            .HasColumnType("INT")
            .IsRequired();
        });

        builder.OwnsOne(i => i.TotalPrice, b =>
        {
            b.Property(p => p.Value)
            .HasColumnName("TotalPrice")
            .HasColumnType("DECIMAL(18,2)")
            .IsRequired();
        });

        builder
            .HasOne(x => x.Order)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}

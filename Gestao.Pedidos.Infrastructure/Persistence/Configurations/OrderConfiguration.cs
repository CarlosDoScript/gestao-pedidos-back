
namespace Gestao.Pedidos.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .ToTable(nameof(Order));

        builder
            .HasKey(x => x.Id);

        builder
            .HasIndex(x => x.CustomerId);

        builder
            .Property(x => x.OrderDate)
            .HasColumnName("OrderDate")
            .HasDefaultValueSql("GETUTCDATE()")
            .HasColumnType("DATETIME")
            .IsRequired();

        builder.OwnsOne(o => o.TotalAmount, b =>
        {
            b.Property(m => m.Value)
            .HasColumnName("TotalAmount")
            .HasColumnType("DECIMAL(18,2)")
            .IsRequired();
        });

        builder.Property(o => o.Status)
            .HasColumnName("Status")
            .HasConversion<string>()
            .HasMaxLength(250)
            .IsRequired();

        builder
            .HasOne(x => x.Customer)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.CustomerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
namespace Gestao.Pedidos.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .ToTable(nameof(Product));

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .HasColumnType("VARCHAR")
            .HasMaxLength(250)
            .IsRequired();

        builder.OwnsOne(p => p.Price, b =>
        {
            b.Property(m => m.Value)
            .HasColumnName("Price")
            .HasColumnType("DECIMAL(18,2)")
            .IsRequired();
        });
    }
}
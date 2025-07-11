namespace Gestao.Pedidos.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .ToTable(nameof(Customer));

        builder
            .HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(250)
            .IsRequired();

        builder.OwnsOne(c => c.Email, b =>
        {
            b.Property(p => p.Address)
            .HasColumnName("Email")
            .HasColumnType("VARCHAR")
            .HasMaxLength(250)
            .IsRequired();
        });

        builder.OwnsOne(c => c.Phone, b =>
        {
            b.Property(p => p.Number)
            .HasColumnName("Phone")
            .HasColumnType("VARCHAR")
            .HasMaxLength(250)
            .IsRequired();
        });
    }
}
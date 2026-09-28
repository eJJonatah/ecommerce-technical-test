using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TEcomerc.Domain.Entities;

namespace TEcomerc.Infraestructure.Sqlite.Configurations;

public sealed class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        _= builder.HasKey(x => x.Id);

        _= builder.HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey("OrderId")
            .IsRequired();
    }
}
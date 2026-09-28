using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TEcomerc.Domain.Entities;

namespace TEcomerc.Infraestructure.Sqlite.Configurations;

public sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemEntity>
{
    public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        _= builder.HasKey(x => x.Id);
    }
}
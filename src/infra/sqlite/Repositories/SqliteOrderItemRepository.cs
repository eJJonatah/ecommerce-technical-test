using Microsoft.EntityFrameworkCore;
using TEcomerc.Application.Persistency;
using TEcomerc.Domain.Entities;
namespace TEcomerc.Infraestructure.Sqlite.Repositories;
using TEcomerc.Infraestructure.Sqlite.Configurations;

public sealed class SqliteOrderItemRepository : OrderItemRepository, IDisposable
{
    readonly DbSet<OrderItemEntity> db;
    readonly ApplicationDbContext transaction;

    public SqliteOrderItemRepository(ApplicationDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
        transaction = dbContext;
        db = dbContext.OrderItems;
    }

    public void Dispose() { transaction.Dispose(); }

    public async Task<Guid> Add(OrderItem instance)
    {
        ArgumentNullException.ThrowIfNull(instance);
        var asEntity = (instance as OrderItemEntity) ?? OrderItemEntity.Create(instance);

        try
        {
            var addition = await db.AddAsync(asEntity, CancellationToken.None);
            _= await transaction.SaveChangesAsync();
            return addition.Entity.Id;
        }
        catch
        {
            Dispose();
            throw;
        }
    }

    public async Task<OrderItem?> Read(Guid id, CancellationToken ct)
    {
        return await db.Where(o=> o.Id == id).FirstOrDefaultAsync(ct);
    }
}
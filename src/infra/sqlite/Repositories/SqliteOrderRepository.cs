using Microsoft.EntityFrameworkCore;
using TEcomerc.Application.Commands;
using TEcomerc.Application.Persistency;
using TEcomerc.Domain.Entities;
namespace TEcomerc.Infraestructure.Sqlite.Repositories;
using TEcomerc.Infraestructure.Sqlite.Configurations;

public sealed class SqliteOrderRepository : OrderRepository, IDisposable
{
    readonly DbSet<OrderEntity> db;
    readonly ApplicationDbContext transaction;

    public SqliteOrderRepository(ApplicationDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
        transaction = dbContext;
        db = dbContext.Orders;
    }

    public async Task<Guid> Add<TItem>(Order<TItem> instance) where TItem : OrderItem
    {
        ArgumentNullException.ThrowIfNull(instance);
        var asEntity = (instance as OrderEntity) ?? OrderEntity.Create(instance);

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

    public void Dispose() { transaction.Dispose(); }

    public IAsyncEnumerable<OrderEntity> List(ListOrderQuery query, CancellationToken ct)
    {
        var dbQuery = db.AsNoTracking();
        var preliminarQuery = dbQuery.OrderByDescending(x => x.Id)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize);

        return query.IncludeItems
            ? preliminarQuery.Include(o => o.Items).AsAsyncEnumerable()
            : preliminarQuery.AsAsyncEnumerable();
    }

    public Task<OrderEntity?> Read(Guid id, CancellationToken ct) { return db.Where(x => x.Id == id).FirstOrDefaultAsync(ct); }

    public async Task Update<TItem>(Guid id, RepositoryCommandUpdate<Order<TItem>> update) where TItem : OrderItem
    {
        ArgumentNullException.ThrowIfNull(update);
        var order = db.Entry(await db.SingleAsync(x => x.Id == id));

        order.Property(o => o.CreatedAt).Apply(update.GetUpdateValorFor(o => o.CreatedAt));
        order.Property(o => o.CustomerId).Apply(update.GetUpdateValorFor(o => o.CustomerId));
        order.Property(o => o.Status).Apply(update.GetUpdateValorFor(o => o.Status));

        _= await transaction.SaveChangesAsync();
    }
}
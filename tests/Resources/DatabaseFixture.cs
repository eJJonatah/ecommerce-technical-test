using TEcomerc.Infraestructure.Sqlite.Configurations;
using Microsoft.EntityFrameworkCore;

namespace TEcomerc.Tests.Resources;

sealed class DatabaseFixture : IAsyncLifetime
{
    internal ApplicationDbContext Db { get; set; } = default!;

    public async Task InitializeAsync()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("Data Source=ecomerc.db")
            .Options;

        Db = new ApplicationDbContext(options);

        if (File.Exists("ecomerc.db")) {
            _= await Db.Database.EnsureDeletedAsync();
            File.Delete("ecomerc.db-shm");
            File.Delete("ecomerc.db-wal");
        }

        _= await Db.Database.EnsureCreatedAsync();

        // Recreate it using your EF Core migrations
        await Db.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        _= await Db.Database.EnsureDeletedAsync();
        await Db.DisposeAsync();
    }
}
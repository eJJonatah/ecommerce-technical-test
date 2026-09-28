using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TEcomerc.Api.Services;
using TEcomerc.Application.Persistency;
using TEcomerc.Infraestructure.Sqlite.Configurations;
using TEcomerc.Infraestructure.Sqlite.Repositories;

namespace TEcomerc.Tests.Resources;

static class ServiceCollectionProvider
{
	public static IServiceProvider CreateServiceCollection()
	{
		IServiceCollection svc = new ServiceCollection();
		return svc.AddScoped(typeof(Application.Observability.ILogger<>), typeof(SerilogLoggerProxy<>))
			.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=ecomerc.db"))
			.AddScoped<OrderItemRepository, SqliteOrderItemRepository>()
			.AddScoped<OrderRepository, SqliteOrderRepository>()
			.BuildServiceProvider();
	}
}

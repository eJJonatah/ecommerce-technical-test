#pragma warning disable CA1305 // Specify IFormatProvider

using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using TEcomerc.Api.Services;
using TEcomerc.Application;
using TEcomerc.Application.Persistency;
using TEcomerc.Infraestructure.Sqlite.Configurations;
using TEcomerc.Infraestructure.Sqlite.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console()
);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApplicationPartManager(manager =>
    {
        manager.FeatureProviders.Add(
            new InternalControllerFeatureProvider());
    });

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(
        typeof(ApplicationAssemblyMarker).Assembly));

builder.Services.AddScoped<OrderRepository, SqliteOrderRepository>();
builder.Services.AddScoped<OrderItemRepository, SqliteOrderItemRepository>();

builder.Services.AddValidatorsFromAssembly(
    typeof(ApplicationAssemblyMarker).Assembly);

builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("this-is-a-test-secret-key-at-least-32-chars")
            ),
            ValidIssuer = "MyApi",
            ValidAudience = "MyApi",
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true
        };
    });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")
    ?? throw new InvalidOperationException("Default connection string was not found."))
);

builder.Services.AddScoped(typeof(TEcomerc.Application.Observability.ILogger<>), typeof(SerilogLoggerProxy<>));

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await db.Database.EnsureCreatedAsync();
await db.Database.MigrateAsync();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync().ConfigureAwait(false);

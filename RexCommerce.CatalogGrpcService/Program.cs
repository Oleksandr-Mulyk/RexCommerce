using Microsoft.EntityFrameworkCore;
using RexCommerce.CatalogGrpcService.Data;
using RexCommerce.CatalogGrpcService.Services;
using RexCommerce.RepositoryLibrary;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddTransient<ICategory, Category>();
builder.Services.AddTransient<IProduct, Product>();
builder.Services.AddTransient<IRepository<IProduct>, ProductRepository>();
builder.Services.AddTransient<IRepository<ICategory>, CategoryRepository>();

var connectionString = builder.Configuration.GetConnectionString("catalogdb")
    ?? throw new InvalidOperationException("Connection string 'catalogdb' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

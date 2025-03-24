using RexCommerce.CatalogGrpcService.Data;
using RexCommerce.CatalogGrpcService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddTransient<ICategory, Category>();
builder.Services.AddTransient<IProduct, Product>();

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

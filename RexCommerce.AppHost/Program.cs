var builder = DistributedApplication.CreateBuilder(args);

var catalogSql = builder.AddSqlServer("catalog-sql")
    .WithLifetime(ContainerLifetime.Persistent);

catalogSql.AddDatabase("catalogdb");

//var apiService = builder.AddProject<Projects.RexCommerce_ApiService>("apiservice");

builder.AddProject<Projects.RexCommerce_CatalogGrpcService>("catalog-grpc-service")
    .WithReference(catalogSql)
    .WaitFor(catalogSql);

builder.AddProject<Projects.RexCommerce_Web>("webfrontend")
    .WithExternalHttpEndpoints();
    //.WithReference(apiService)
    //.WaitFor(apiService);

builder.Build().Run();

var builder = DistributedApplication.CreateBuilder(args);

//var apiService = builder.AddProject<Projects.RexCommerce_ApiService>("apiservice");

builder.AddProject<Projects.RexCommerce_Web>("webfrontend")
    .WithExternalHttpEndpoints();
    //.WithReference(apiService)
    //.WaitFor(apiService);

builder.AddProject<Projects.RexCommerce_CatalogGrpcService>("rexcommerce-cataloggrpcservice");
    //.WithReference(apiService)
    //.WaitFor(apiService);

builder.Build().Run();

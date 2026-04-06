var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.MFO_CatalogService_API>("api")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health");

builder.Build().Run();

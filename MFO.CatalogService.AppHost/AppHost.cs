var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent);

var catalogDb = sql.AddDatabase("CatalogDB");

var api = builder.AddProject<Projects.MFO_CatalogService_API>("api")
    .WithExternalHttpEndpoints()
    .WithReference(catalogDb)
    .WaitFor(catalogDb)
    .WithHttpHealthCheck("/health");

builder.Build().Run();

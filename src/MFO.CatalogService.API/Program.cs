using Elastic.Channels;
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using MediatR;
using MFO.CatalogService.API.Middlewares;
using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Application.Common.Mapping;
using MFO.CatalogService.Application.Features.Products.Queries.GetProductById;
using MFO.CatalogService.Application.Services;
using MFO.CatalogService.Infrastructure.Persistence;
using MFO.CatalogService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NSwag;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddOpenApiDocument(options =>
{
    options.PostProcess = document =>
    {
        document.Info = new OpenApiInfo
        {
            Version = "v1",
            Title = "Catalog Service API",
            Description = "An ASP.NET Core Web API for managing Products, Categories, etc.",
            //TermsOfService = "https://example.com/terms",
            Contact = new OpenApiContact
            {
                Name = "Mihai Negrisan",
                Url = "https://github.com/mihainegrisan/MFO.CatalogService"
            },
            //License = new OpenApiLicense
            //{
            //    Name = "Example License",
            //    Url = "https://example.com/license"
            //}
        };
    };
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetProductByIdQueryHandler).Assembly));

builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new CatalogServiceProfile()));

//builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>(ServiceLifetime.Transient);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviorMiddleware<,>));

builder.Services.AddControllers();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ISkuSequenceRepository, SkuSequenceRepository>();

builder.Services.AddScoped<ISkuGenerator, SkuGenerator>();

builder.Services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CatalogContext")));

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .MinimumLevel.Information()

    .Enrich.FromLogContext()
    .Enrich.WithProperty("Service", "MFO.CatalogService")
    .WriteTo.Console()
    //.WriteTo.File("logs/catalogservice-.log", rollingInterval: RollingInterval.Day)
    .WriteTo.Elasticsearch(
        [new Uri("http://localhost:9200")],        // ES endpoint(s)
        opts =>
        {
            // Use a data stream (recommended). Structure: type, dataset, namespace.
            // This will target a datastream like: logs-catalogservice-dev
            opts.DataStream = new DataStreamName("logs", "catalogservice", "dev");

            // How the sink should attempt bootstrap templates: None / Silent / Failure
            // Silent = try but don't fail app if templates can't be installed.
            opts.BootstrapMethod = BootstrapMethod.Silent;

            // Optional: tune the in-memory channel (backpressure/batching). Keep defaults unless you need to tweak.
            opts.ConfigureChannel = channelOptions =>
            {
                // set an empty BufferOptions (don't try to use properties that may have been removed)
                channelOptions.BufferOptions = new BufferOptions();
            };
        },
        transport =>
        {
            // if your ES has auth, configure transport here:
            // transport.Authentication(new BasicAuthentication("user","pass"));
        })
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Add OpenAPI 3.0 document serving middleware
    // Available at: http://localhost:<port>/swagger/v1/swagger.json
    app.UseOpenApi();

    // Add web UIs to interact with the document
    // Available at: http://localhost:<port>/swagger
    app.UseSwaggerUi(); // UseSwaggerUI Protected by if (env.IsDevelopment())
}

app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();

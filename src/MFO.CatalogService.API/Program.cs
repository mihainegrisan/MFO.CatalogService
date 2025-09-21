using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Application.Common.Mapping;
using MFO.CatalogService.Application.Features.Products.Queries.GetProductById;
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

builder.Services.AddControllers();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

builder.Services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CatalogContext")));

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Service", "MFO.CatalogService")
    .WriteTo.Console()
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

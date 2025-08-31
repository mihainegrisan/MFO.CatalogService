using MFO.CatalogService.Application.Common.Mapping;
using MFO.CatalogService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using NSwag;

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

builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new CatalogServiceProfile()));

builder.Services.AddControllers();

builder.Services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CatalogContext")));

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

app.Run();

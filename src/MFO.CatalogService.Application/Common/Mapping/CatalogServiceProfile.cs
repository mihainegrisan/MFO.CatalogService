using AutoMapper;
using MFO.CatalogService.Domain.Entities;
using MFO.Contracts.Catalog.DTOs.Brand;
using MFO.Contracts.Catalog.DTOs.Category;
using MFO.Contracts.Catalog.DTOs.Company;
using MFO.Contracts.Catalog.DTOs.Product;

namespace MFO.CatalogService.Application.Common.Mapping;

public class CatalogServiceProfile : Profile
{
    public CatalogServiceProfile()
    {
        // Entity → DTO
        CreateMap<Product, GetProductDto>();
        CreateMap<Category, GetCategoryDto>();
        CreateMap<Brand, GetBrandDto>();
        CreateMap<Company, GetCompanyDto>();

        // DTO → Entity(for create / update)
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();

        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();

        CreateMap<CreateBrandDto, Brand>();
        CreateMap<UpdateBrandDto, Brand>();

        CreateMap<CreateCompanyDto, Company>();
        CreateMap<UpdateCompanyDto, Company>();
    }
}


using AutoMapper;
using MFO.CatalogService.Application.DTOs.Brand;
using MFO.CatalogService.Application.DTOs.Category;
using MFO.CatalogService.Application.DTOs.Product;
using MFO.CatalogService.Domain.Entities;

namespace MFO.CatalogService.Application.Common.Mapping;

public class CatalogServiceProfile : Profile
{
    public CatalogServiceProfile()
    {
        // Entity → DTO
        CreateMap<Product, GetProductDto>();
        CreateMap<Category, GetCategoryDto>();
        CreateMap<Brand, GetBrandDto>();

        // DTO → Entity(for create / update)
        CreateMap<CreateProductDto, Product>();
        //CreateMap<UpdateProductDto, Product>();
    }
}


using AutoMapper;
using Catalog.API.Contracts.Commands;
using Catalog.API.Contracts.Views;
using Product = Catalog.API.Contracts.Views.Product;

namespace Catalog.API.Configuration.Mapper
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<Domain.Aggregates.Product, Product>().ReverseMap();
            CreateMap<Domain.Aggregates.CatalogExport, CatalogExport>().ReverseMap();

            CreateMap<Domain.Contracts.Commands.UpdateProductImage, UpdateProductImage>().ReverseMap();
            CreateMap<Domain.Contracts.Commands.UpdateProduct, UpdateProduct>().ReverseMap();
        }
    }
}

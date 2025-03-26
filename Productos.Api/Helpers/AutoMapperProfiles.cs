using AutoMapper;
using Productos.Api.DataBase.Entities;
using Productos.Api.Dtos.Products;

namespace Productos.Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {


            CreateMap<ProductCreateDto, ProductsEntity>();
            CreateMap<ProductEditDto, ProductsEntity>();
            CreateMap<ProductsEntity, ProductDto>();
            CreateMap<ProductsEntity, ProductActionResponseDto>();
        }
    }
}

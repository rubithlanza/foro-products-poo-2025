using Productos.Api.Dtos.Common;
using Productos.Api.Dtos.Products;

namespace Productos.Api.Services.Interface
{
    public interface IProductService
    {
        Task<ResponseDtos<ProductActionResponseDto>> CreateAsync(ProductCreateDto dto);
        Task<ResponseDtos<ProductActionResponseDto>> DeleteAsync(Guid id);
        Task<ResponseDtos<ProductActionResponseDto>> EditAsync(ProductEditDto dto, Guid id);
        Task<ResponseDtos<List<ProductDto>>> GetListAsync();
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Productos.Api.Constants;
using Productos.Api.DataBase.Entities;
using Productos.Api.Dtos.Common;
using Productos.Api.Dtos.Products;
using Productos.Api.Services.Interface;

namespace Productos.Api.Services
{
    public class ProductService : IProductService
    {

        private readonly ProductsDbContext _context;
        private readonly IMapper _mapper;
        public ProductService(ProductsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Para en listar
        public async Task<ResponseDtos<List<ProductDto>>> GetListAsync()
        {
            var products = await _context.Products.OrderBy(x => x.Name).ToListAsync();

            // Verifica que hay datos antes de mapear
            if (products == null || !products.Any())
            {
                return new ResponseDtos<List<ProductDto>>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "No se encontraron productos",
                    Data = null
                };
            }

            var productsDtos = _mapper.Map<List<ProductDto>>(products);

            return new ResponseDtos<List<ProductDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro obtenidos correctamente",
                Data = productsDtos
            };
        }

        //Para crear un producto 
        public async Task<ResponseDtos<ProductActionResponseDto>> CreateAsync(ProductCreateDto dto)
        {
            var productEntity = _mapper.Map<ProductsEntity>(dto);
            _context.Products.Add(productEntity);
            await _context.SaveChangesAsync();

            return new ResponseDtos<ProductActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro creado correctamente",
                Data = _mapper.Map<ProductActionResponseDto>(productEntity)
            };

        }

        //Para editar el producto
        public async Task<ResponseDtos<ProductActionResponseDto>> EditAsync(ProductEditDto dto, Guid id)
        {
            var productEntity = await _context.Products.FindAsync(id);

            if (productEntity is null)
            {
                return new ResponseDtos<ProductActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }

            _mapper.Map<ProductEditDto, ProductsEntity>(dto, productEntity);
            _context.Products.Update(productEntity);
            await _context.SaveChangesAsync();

            return new ResponseDtos<ProductActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro Editado",
                Data = _mapper.Map<ProductActionResponseDto>(productEntity)
            };

        }

        //Para eliminar 
        public async Task<ResponseDtos<ProductActionResponseDto>> DeleteAsync(Guid id)
        {
            var countryEntity = await _context.Products.FindAsync(id);

            if (countryEntity == null)
            {
                return new ResponseDtos<ProductActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }

            _context.Products.Remove(countryEntity);
            await _context.SaveChangesAsync();

            return new ResponseDtos<ProductActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro Eliminado",
                //Data = _mapper.Map 
            };
        }


    }
    }

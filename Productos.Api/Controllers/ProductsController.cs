using Microsoft.AspNetCore.Mvc;
using Productos.Api.DataBase.Entities;
using Productos.Api.Dtos.Common;
using Productos.Api.Dtos.Products;
using Productos.Api.Services.Interface;

namespace Productos.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        //Obtener lista
        [HttpGet]
        public async Task<ActionResult<ResponseDtos<List<ProductsEntity>>>> GetList()
        {
            var response = await _productService.GetListAsync();

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        //Para la creacion 
        [HttpPost]
        public async Task<ActionResult<ResponseDtos<ProductActionResponseDto>>> Post([FromBody] ProductCreateDto dto)
        {
            var response = await _productService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        //Para editar

        [HttpPut("{id}")] // Put porque se edita todo
        public async Task<ActionResult<ResponseDtos<ProductActionResponseDto>>> Edit([FromBody] ProductEditDto dto, Guid id)
        {
            var response = await _productService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, response);
        }


        //Para Eliminar
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDtos<ProductActionResponseDto>>> Delete(Guid id)
        {
            var response = await _productService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}

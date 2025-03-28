using Microsoft.AspNetCore.Mvc;
using Moq;
using Productos.Api.Constants;
using Productos.Api.Controllers;
using Productos.Api.Dtos.Common;
using Productos.Api.Dtos.Products;
using Productos.Api.Services.Interface;
using Xunit;

namespace Productos.Api.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _mockService; //Para el uso del controller
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockService = new Mock<IProductService>(); //Nuevo
            _controller = new ProductsController(_mockService.Object);//Inyeccion
        }
        //Para verificar si se ha creado
        [Fact]
        public async Task Post_ReturnsCreated_WhenProductIsValid()
        {
            // Arrange
            _mockService.Setup(x => x.CreateAsync(It.IsAny<ProductCreateDto>()))
                .ReturnsAsync(new ResponseDtos<ProductActionResponseDto>
                {
                    StatusCode = HttpStatusCode.CREATED,
                    Status = true,
                    Data = new ProductActionResponseDto { Name = "Harina" }
                });

            // Act
            var result = await _controller.Post(new ProductCreateDto()); //Llamando vacio

            // Assert
            var actionResult = Assert.IsType<ActionResult<ResponseDtos<ProductActionResponseDto>>>(result);
            var createdAtActionResult = Assert.IsType<ObjectResult>(actionResult.Result); 

            Assert.Equal(201, createdAtActionResult.StatusCode);
            Assert.NotNull(createdAtActionResult.Value); //Retorno no es nulo
        }


        //Eliminacion 
        [Fact]
        public async Task Delete_ReturnsOk_WhenProductExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mockService.Setup(x => x.DeleteAsync(productId))
                .ReturnsAsync(new ResponseDtos<ProductActionResponseDto>
                {
                    StatusCode = HttpStatusCode.OK,
                    Status = true,
                    Message = "Producto eliminado",
                    Data = new ProductActionResponseDto { Id = productId, Name = "Harina eliminado" }
                });

            // Act
            var result = await _controller.Delete(productId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ResponseDtos<ProductActionResponseDto>>>(result);
            var okResult = Assert.IsType<ObjectResult>(actionResult.Result);

            Assert.Equal(200, okResult.StatusCode);
            var response = Assert.IsType<ResponseDtos<ProductActionResponseDto>>(okResult.Value);
            Assert.Equal(productId, response.Data.Id);
        }

        //Para editar el elemento
        [Fact]
        public async Task Edit_ReturnsOk_WhenProductIsUpdated()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var editDto = new ProductEditDto { Name = "Harina Actualizado", Price = "200", Stock = "5" };

            _mockService.Setup(x => x.EditAsync(editDto, productId))
                .ReturnsAsync(new ResponseDtos<ProductActionResponseDto>
                {
                    StatusCode = HttpStatusCode.OK,
                    Status = true,
                    Message = "Producto actualizado",
                    Data = new ProductActionResponseDto { Id = productId, Name = "Harina Actualizado" }
                });

            // Act
            var result = await _controller.Edit(editDto, productId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ResponseDtos<ProductActionResponseDto>>>(result);
            var okResult = Assert.IsType<ObjectResult>(actionResult.Result);

            Assert.Equal(200, okResult.StatusCode);
            var response = Assert.IsType<ResponseDtos<ProductActionResponseDto>>(okResult.Value);
            Assert.Equal("Harina Actualizado", response.Data.Name);
        }
    }
}




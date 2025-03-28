using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Productos.Api.Constants;
using Productos.Api.Dtos.Products;
using Productos.Api.Services;
using Productos.Api.Tests.Helpers;
using Xunit;

namespace Productos.Api.Tests.Services
{
    public class ProductServiceTests : TestBase //  Hereda de TestBase
    {
        [Fact]
        public async Task GetListAsync_ReturnsAllProducts()
        {
            // Arrange
            var service = new ProductService(_context, _mapper); // Usa _context y _mapper de TestBase

            // Act
            var result = await service.GetListAsync();

            // Assert
            Assert.True(result.Status);
            Assert.Equal(2, result.Data.Count); // Verifica los 2 productos de SeedDatabase()
        }

        [Fact]
        public async Task CreateAsync_AddsNewProduct()
        {
            // Arrange
            var service = new ProductService(_context, _mapper);
            var newProduct = new ProductCreateDto { Name = "Harina", Price = "30", Stock = "300" };

            // Act
            var result = await service.CreateAsync(newProduct);

            // Assert
            Assert.True(result.Status);
            Assert.Equal("Harina", result.Data.Name);
            Assert.Equal(3, _context.Products.Count()); // 2 iniciales + 1 nuevo
        }

        [Fact]
        public async Task DeleteAsync_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid(); // ID que no existe en la DB
            var service = new ProductService(_context, _mapper);

            // Act
            var result = await service.DeleteAsync(nonExistentId);

            // Assert
            Assert.False(result.Status); // Verifica que la operación falló
            Assert.Equal(HttpStatusCode.NOT_FOUND, result.StatusCode); // Código 404
            Assert.Equal("Registro no encontrado", result.Message);
        }
    }
}
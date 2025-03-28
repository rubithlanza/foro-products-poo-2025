using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Productos.Api.DataBase.Entities;
using Productos.Api.Helpers;
using Xunit;

namespace Productos.Api.Tests.Helpers
{
    public abstract class TestBase : IDisposable
    {
        protected readonly ProductsDbContext _context;
        protected readonly IMapper _mapper;

        public TestBase()
        {
            // 1. Configurar base de datos en memoria
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Nombre único
                .Options;

            _context = new ProductsDbContext(options);
            _context.Database.EnsureCreated(); // Crea la DataBase

            // 2. Configurar AutoMapper 
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfiles>();
            });

            _mapper = mapperConfig.CreateMapper();

            // 3. (Opcional) Agregar datos iniciales
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _context.Products.AddRange(
                new ProductsEntity { Id = Guid.NewGuid(), Name = "Margarina", Price = "100", Stock = "10" },
                new ProductsEntity { Id = Guid.NewGuid(), Name = "Huevos", Price = "200", Stock = "20" }
            );
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted(); // Limpia la DB después de cada prueba
            _context.Dispose();
        }
    }
}

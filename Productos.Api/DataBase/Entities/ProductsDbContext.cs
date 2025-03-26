using Microsoft.EntityFrameworkCore;

namespace Productos.Api.DataBase.Entities
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions options) : base(options) 
        {
            
        }

        public DbSet<ProductsEntity> Products { get; set; }
    }
}

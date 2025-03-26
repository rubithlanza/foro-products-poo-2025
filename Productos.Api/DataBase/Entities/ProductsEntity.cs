using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Productos.Api.DataBase.Entities
{
    [Table("products")]
    public class ProductsEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Required]
        [Column("price")]
        public string Price { get; set; }

        [Required]
        [Column("stock")]
        public string Stock { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Productos.Api.Dtos.Products
{
    public class ProductCreateDto
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(13, MinimumLength = 3, ErrorMessage = "El campo {0} debe tener un minimo de {2} y una maximo de {1} caracteres.")]
        public string Name { get; set; }

        [Display(Name = "Descripcion del producto")]
        public string Description { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(9, MinimumLength = 1, ErrorMessage = "El campo {0} debe tener un minimo de {2} y una maximo de {1} caracteres.")]
        public string Price { get; set; }

        [Display(Name = "Cantidad disponible")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(9, MinimumLength = 1, ErrorMessage = "El campo {0} debe tener un minimo de {2} y una maximo de {1} caracteres.")]

        public string Stock { get; set; }

    }
}

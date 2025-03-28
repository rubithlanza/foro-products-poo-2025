﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Productos.Api.Dtos.Products
{
    public class ProductDto
    {
       
        public Guid Id { get; set; }

     
        public string Name { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public string Stock { get; set; }
    }
}

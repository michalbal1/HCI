using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace RazorPagesMovie.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; } 

        public int QuantityInStock { get; set; }

        [Required]
        public decimal Price { get; set; }

       
        public string? ImageUrl { get; set; }

      
        public ICollection<ProductCategory>? ProductCategories{ get; set; }
    }
}
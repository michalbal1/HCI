using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace RazorPagesMovie.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        [Required]
        public int ProductId {get;  set;}
        public Product? Product { get; set;}

        [Required]
        public int CategoryId { get; set;}
        public Category? Category { get; set;}
    }
}
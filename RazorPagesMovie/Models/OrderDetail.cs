using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace RazorPagesMovie.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

     
      
        
      

        
    }
}
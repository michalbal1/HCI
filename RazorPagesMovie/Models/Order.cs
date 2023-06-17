using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace RazorPagesMovie.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public string? Status { get; set; }

       
        public int? UserId { get; set; }

        public User? User { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

       
        public string? GuestEmail { get; set; }
        public string? GuestShippingAddress { get; set; }
        





    }
}
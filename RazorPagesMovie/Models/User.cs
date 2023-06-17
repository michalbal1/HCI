using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace RazorPagesMovie.Models
{
    public class User
    {
        public int Id{ get; set;}

        [Required]
        public string? Username { get; set;}
        [Required]
        public string? Password { get; set;}
        [Required]
        public string? Email { get; set;}
        public string? Address { get; set;}
        public string? Phone { get; set;}
        [Required]
        public string? Role { get; set;}
        public ICollection<Order> Orders { get; set;}


        

    }
}

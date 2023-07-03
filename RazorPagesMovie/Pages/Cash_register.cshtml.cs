using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using RazorPagesMovie.Data;
using System.Net.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RazorPagesMovie.Pages
{
    public class Cash_registerModel : PageModel
    {
        
        [BindProperty]
        [Required(ErrorMessage = "To Pole jest wymagane.")]
        public string FirstName { get; set; } = string.Empty;
        [BindProperty]
        [Required(ErrorMessage = "To Pole jest wymagane.")]
        public string LastName { get; set; } = string.Empty;
        [BindProperty]
        [Required(ErrorMessage = "To Pole jest wymagane.")]
        public string Email { get; set; } = string.Empty;
        [BindProperty]
        [Required(ErrorMessage = "To Pole jest wymagane.")]
        public string Tel { get; set; } = string.Empty;
        [BindProperty]
        [Required(ErrorMessage = "To Pole jest wymagane.")]
        public string Adres { get; set; } = string.Empty;
        [BindProperty]
        
        public int ShippingPrice { get; set; } 




        public List<CartItem> CartItems { get; set; }

        private readonly RazorPagesMovieContext _dbContext;

        public Cash_registerModel(RazorPagesMovieContext dbContext)
        {

            _dbContext = dbContext;

        }
        public void OnGet()
        {
          

            var sessionCart = HttpContext.Session.GetString("Cart");
            if (sessionCart != null)
            {
                CartItems = JsonConvert.DeserializeObject<List<CartItem>>(sessionCart);



            }

        }
        public async Task<IActionResult> OnPostAsync()
        {
            var sessionCart = HttpContext.Session.GetString("Cart");
            var sessionId = HttpContext.Session.GetInt32("Id");

            if (sessionCart != null)
            {
                CartItems = JsonConvert.DeserializeObject<List<CartItem>>(sessionCart);



            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (sessionId != null)
            {
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    Status = "New",
                    GuestEmail = Email,
                    UserId = sessionId,
                    GuestShippingAddress = Adres,
                    ShippingPrice = ShippingPrice
                };
                foreach (var cartItem in CartItems)
                {
                    order.OrderDetails.Add(new OrderDetail
                    {
                        Quantity = cartItem.Quantity,
                        Price = cartItem.Price,
                        ProductId = cartItem.ProductId
                    });
                    var product = _dbContext.Products.SingleOrDefault(p => p.Id == cartItem.ProductId);


                    if (product != null)
                    {
                        product.QuantityInStock -= cartItem.Quantity;
                    }
                }


                _dbContext.Orders.Add(order);
                await _dbContext.SaveChangesAsync();
                HttpContext.Session.Remove("Cart");
                return RedirectToPage("/Store");
            }
            else
            {
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    Status = "New",
                    GuestEmail = Email,
                   
                    GuestShippingAddress = Adres,
                    ShippingPrice = ShippingPrice
                };
                foreach (var cartItem in CartItems)
                {
                    order.OrderDetails.Add(new OrderDetail
                    {
                        Quantity = cartItem.Quantity,
                        Price = cartItem.Price,
                        ProductId = cartItem.ProductId
                    });
                    var product = _dbContext.Products.SingleOrDefault(p => p.Id == cartItem.ProductId);


                    if (product != null)
                    {
                        product.QuantityInStock -= cartItem.Quantity;
                    }
                }


                _dbContext.Orders.Add(order);
                await _dbContext.SaveChangesAsync();
                HttpContext.Session.Remove("Cart");
                return RedirectToPage("/Store");
            }
        }
    }
}
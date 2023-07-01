using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace RazorPagesMovie.Pages
{
    public class StoreModel : PageModel
    {
        private readonly RazorPagesMovieContext _dbContext;
        public List<Product>? Products { get; set; }

        [BindProperty]
        public int ProductId { get; set; }

        [BindProperty]
        public int Quantity { get; set; }
        public StoreModel(RazorPagesMovieContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            Products = await _dbContext.Products.ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAddToCartAsync()
        {
            Products = await _dbContext.Products.ToListAsync();
            string jsonCart = HttpContext.Session.GetString("Cart");
            List<CartItem> cart;
            if (string.IsNullOrEmpty(jsonCart))
            {
                cart = new List<CartItem>();
            }
            else
            {
                cart = JsonConvert.DeserializeObject<List<CartItem>>(jsonCart);
            }

            Product product = Products.FirstOrDefault(p => p.Id == ProductId);
            if (product != null)
            {
                CartItem cartItem = cart.FirstOrDefault(i => i.ProductId == ProductId);
                if (cartItem != null)
                {
                    if (Quantity != 0)
                    {

                        cartItem.Quantity += Quantity;
                    }
                    else { cartItem.Quantity++; }
                }
                else
                {
                    if (Quantity != 0)
                    {
                        cart.Add(new CartItem { ProductId = ProductId, Quantity = Quantity, Name = product.Name, Price=product.Price });
                    } else {
                        cart.Add(new CartItem { ProductId = ProductId, Quantity = 1, Name = product.Name, Price=product.Price });
                           }
                }
            }

           
            jsonCart = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString("Cart", jsonCart);

            return RedirectToAction("/Store");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages
{
    public class ShopCartModel : PageModel
    {
       
        public List<CartItem> CartItems { get; set; }
        public void OnGet()
        {
            var sessionCart = HttpContext.Session.GetString("Cart");
            if (sessionCart != null)
            {
                CartItems = JsonConvert.DeserializeObject<List<CartItem>>(sessionCart);
                var cartItemsCount = CartItems.Count;
                HttpContext.Session.SetInt32("CartItemsCount", cartItemsCount);
                
            }
            else
            {

                HttpContext.Session.SetInt32("CartItemsCount", 0);
            }
        }
    }
}

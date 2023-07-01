using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Data;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace RazorPagesMovie.Pages
{
    public class UsersModel : PageModel
    {
        public ShopCartModel shopcart { get; set; }
        public StoreModel StoreModel { get; set; }
        private readonly RazorPagesMovieContext _dbContext;

        public List<User> User { get; set; }

        public UsersModel(RazorPagesMovieContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> OnGet()
        {
            shopcart = new ShopCartModel();
           
            var Id = HttpContext.Session.GetString("Id");
          


            

            return Page();
        }
    }
}
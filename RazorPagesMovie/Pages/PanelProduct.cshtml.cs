using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages
{
    
    public class PanelProductModel : PageModel
    {
        public List<Product>? Products { get; set; }
        private readonly RazorPagesMovieContext _dbContext;
        public PanelProductModel(RazorPagesMovieContext dbContext)
        {

            _dbContext = dbContext;

        }
       

        public async Task<IActionResult> OnGetAsync()
        {
           
            Products = await _dbContext.Products.ToListAsync();

            return Page();
        }
       
    }
}

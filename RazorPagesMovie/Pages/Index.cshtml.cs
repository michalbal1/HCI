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
        private readonly RazorPagesMovieContext _dbContext;

        public List<User> User { get; set; }

        public UsersModel(RazorPagesMovieContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> OnGet()
        {
            var Id = HttpContext.Session.GetString("Id");
          


            User = await _dbContext.Users.ToListAsync();

            return Page();
        }
    }
}
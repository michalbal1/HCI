using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Pages
{
    public class LogowModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "To Pole jest wymagane.")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "To Pole jest wymagane")]
        [BindProperty]
        public string Pass1 { get; set; } = string.Empty;

        private readonly RazorPagesMovieContext _dbContext;
        public LogowModel(RazorPagesMovieContext dbContext)
        {

            _dbContext = dbContext;

        }
        public void OnGet()
        {
            
        }
         public bool VerifyPassword(string Password, string Pass1)
       {
           var passwordHasher = new PasswordHasher<User>();
           var passwordVerificationResult = passwordHasher.VerifyHashedPassword(null,Password, Pass1);
           return passwordVerificationResult == PasswordVerificationResult.Success;
       }
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {

                return Page();
            }
            
            if (await _dbContext.Users.AnyAsync(u => u.Email == Login || u.Username == Login))
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == Login || u.Username == Login);
                if (user != null &&  Pass1 != null && VerifyPassword(user?.Password, Pass1))
                {
                    HttpContext.Session.SetInt32("Id", user.Id);
                    HttpContext.Session.SetString("Username", user.Username.ToString());
                }
                else
                {
                    ModelState.AddModelError("Pass1", "Nieprawid³owe has³o");
                    return Page();
                }
            }

            return RedirectToPage("/Store");
        }
    }
}

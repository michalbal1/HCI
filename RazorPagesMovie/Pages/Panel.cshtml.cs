using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Pages
{
    public class PanelModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "To Pole jest wymagane.")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "To Pole jest wymagane")]
        [BindProperty]
        public string Pass1 { get; set; } = string.Empty;

        private readonly RazorPagesMovieContext _dbContext;
        public PanelModel(RazorPagesMovieContext dbContext)
        {

            _dbContext = dbContext;

        }

        public void OnGet()
        {
        }
        public bool VerifyPassword(string Password, string Pass1)
        {
            var passwordHasher = new PasswordHasher<User>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(null, Password, Pass1);
            return passwordVerificationResult == PasswordVerificationResult.Success;
        }
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {

                return Page();
            }

            if (Login=="Admin")
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == "Admin");
                if (user.Role=="Admin" && user != null && Pass1 != null && VerifyPassword(user?.Password, Pass1))
                {
                    HttpContext.Session.SetString("Role", user.Role.ToString());
                    return RedirectToPage("/MainPanel");
                }
                else
                {
                    ModelState.AddModelError("Pass1", "z�e has�o");
                    return Page();
                }
            }
            else
            {
                ModelState.AddModelError("Login", "Brak uprawnie�");
                return Page();
            }

            
        }
    }
}

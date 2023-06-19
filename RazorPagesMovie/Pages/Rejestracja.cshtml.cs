using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace RazorPagesMovie.Pages
{
    public class RejestracjaModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "To Pole jest wymagane.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "To Pole jest wymagane.")]
        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "To Pole jest wymagane")]
        [BindProperty]
        public string Pass1 { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "To Pole jest wymagane")]
        public string Pass2 { get; set; } = string.Empty;

        [BindProperty]
        public string? Tel { get; set; }
        [BindProperty]
        public string? Adres { get; set; }

        private readonly RazorPagesMovieContext _dbContext;
      
        public RejestracjaModel(RazorPagesMovieContext dbContext)
        {
            
            _dbContext = dbContext;
           
        }



        public void OnGet()
        {
        }

        public string HashPassword(string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            var hashedPassword = passwordHasher.HashPassword(null, password);
            return hashedPassword;
        }


        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {

                return Page();
            }
            if (await _dbContext.Users.AnyAsync(u => u.Email == Email))
            {
                ModelState.AddModelError("Email", "Ten adres email jest ju¿ w u¿yciu.");
                return Page();
            }
            if (await _dbContext.Users.AnyAsync(u => u.Username == Username))
            {
                ModelState.AddModelError("Username", "Ta nazwa jest ju¿ w u¿yciu.");
                return Page();
            }

            if (Pass1 != Pass2)
            {
                ModelState.AddModelError("Pass1", "Has³a nie s¹ takie same."); // spr dzia³anie
                return Page();
            }


            var user = new User
            {
                Username = Username,
                Email = Email,
               // Password = Pass1,
                Phone = Tel,
                Address = Adres,
                Role="Customer"


            };
            var passwordHasher = new PasswordHasher<User>();
            var hashedPassword = passwordHasher.HashPassword(user, Pass1);

            user.Password = hashedPassword;
        

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();


            return RedirectToPage("/Logow");
        }
    }
}

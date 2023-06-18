using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace RazorPagesMovie.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogoutModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet(string Url)
        {
           
            _httpContextAccessor.HttpContext.Session.Clear();

            if (!string.IsNullOrEmpty(Url))
            {
                
                return Redirect(Url);
            }
            else
            {
                
                return RedirectToPage("/Index");
            }
        }
    }
}








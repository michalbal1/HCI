using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Data;

namespace RazorPagesMovie.Pages
{
    public class MainPanelModel : PageModel
    {
        private readonly RazorPagesMovieContext _dbContext;
        public MainPanelModel(RazorPagesMovieContext dbContext)
        {

            _dbContext = dbContext;

        }
        public void OnGet()
        {
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Pages
{
    public class PanelProductEditModel : PageModel
    {
        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Musisz podaæ nazwê")]
        public string Name { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Podaj opis")]
        public string Description { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Podaj iloœæ dostêpnego produktu")]
        public int QuantityInStock { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Podaj cene")]
        public decimal Price { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Wybierz kategorie")]
        public string SelectedCategory { get; set; }
        private readonly RazorPagesMovieContext _dbContext;
        public List<Category> Categories { get; set; }
        public PanelProductEditModel(RazorPagesMovieContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> OnGetAsync(int? productId)
        {
            Categories = await _dbContext.Categories.ToListAsync();

            if (productId == null)
            {
                return NotFound();
            }

            var product = await _dbContext.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                return NotFound();
            }

            
            Name = product.Name;
            Description = product.Description;
            QuantityInStock = product.QuantityInStock;
            Price = product.Price;
            
         

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? productId)
        {
            Categories = _dbContext.Categories.ToList();

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return Page();
            }

            if (productId == null)
            {
                return NotFound();
            }

            var productToUpdate = await _dbContext.Products.Include(p => p.ProductCategories)
                                                            .ThenInclude(pc => pc.Category)
                                                            .FirstOrDefaultAsync(p => p.Id == productId);

            if (productToUpdate == null)
            {
                return NotFound();
            }

            productToUpdate.Name = Name;
            productToUpdate.Description = Description;
            productToUpdate.Price = Price;
            productToUpdate.QuantityInStock = QuantityInStock;
            
            if (SelectedCategory == "wêgiel brunatny")
            {
                productToUpdate.ImageUrl = "~/Images/brunatny.png";
            }
            else if (SelectedCategory == "wêgiel kamienny")
            {
                productToUpdate.ImageUrl = "~/Images/kamieny.png";
            }
            else
            {
                productToUpdate.ImageUrl = "~/Images/drzewny.png";
            }

            
            var selectedCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == SelectedCategory);
            if (selectedCategory != null)
            {
                var productCategoryToUpdate = productToUpdate.ProductCategories.First();
                productCategoryToUpdate.CategoryId = selectedCategory.Id;
            }

            await _dbContext.SaveChangesAsync();

            return RedirectToPage("./PanelProduct"); 
        }
    }
}

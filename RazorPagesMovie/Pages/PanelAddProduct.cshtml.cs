using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Pages
{
    public class PanelAddProductModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage ="Musisz podaæ nazwê")]
        public string Name { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        [Required(ErrorMessage ="Podaj iloœæ dostêpnego produktu")]
        public int QuantityInStock { get; set; }

        [BindProperty]
        [Required(ErrorMessage ="Podaj cene")]
        public decimal Price { get; set; }
        [BindProperty]
        [Required(ErrorMessage ="Wybierz kategorie")]
        public string SelectedCategory { get; set; }
        private readonly RazorPagesMovieContext _dbContext;
        public List<Category> Categories { get; set; }
        

        public PanelAddProductModel(RazorPagesMovieContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async  Task<IActionResult> OnGet ()
        {
            Categories = await _dbContext.Categories.ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync() {
            Categories = _dbContext.Categories.ToList();
            string image = string.Empty;
            if (!ModelState.IsValid)
            {

                return Page();
            }

            if(SelectedCategory=="wêgiel brunatny") {
                image = "~/Images/brunatny.png";
            }else if (SelectedCategory=="wêgiel kamienny")
            {
                image = "~/Images/kamieny.png";
            }
            else
            {
                image = "~/Images/drzewny.png";
            }

            var product = new Product
            {
                Name = Name,
                Description=Description,
                QuantityInStock=QuantityInStock,
                Price=Price,
                ImageUrl=image


            };
            _dbContext.Products.Add(product);
            int count=await _dbContext.SaveChangesAsync();
            int productId = product.Id;

            if (await _dbContext.Categories.AnyAsync(c => c.Name == SelectedCategory))
            {
               
                var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == SelectedCategory);
                if (category != null)
                {


                    var productcat = new ProductCategory
                    {
                        ProductId = productId,
                        CategoryId = category.Id
                    };
                    _dbContext.ProductCategories.Add(productcat);
                    await _dbContext.SaveChangesAsync();
                }
            }

            
            return Page();
        }
    }
}

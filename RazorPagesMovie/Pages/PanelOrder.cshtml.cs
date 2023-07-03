using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;


namespace RazorPagesMovie.Pages
{
    public class PanelOrderModel : PageModel
    {
        private readonly RazorPagesMovieContext _dbContext;
        public List<Order>? Orders{ get; set; }
        public PanelOrderModel(RazorPagesMovieContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet()
        {
            Orders = _dbContext.Orders
        .Include(o => o.OrderDetails)
        .ThenInclude(od => od.Product)
        .ToList();

        }
        public async Task<IActionResult> OnPostUpdateStatusAsync(int id, string NewStatus)
        {
            Orders = _dbContext.Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .ToList();
            var order = await _dbContext.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            order.Status = NewStatus;
            await _dbContext.SaveChangesAsync();

            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int orderId)
        {
            Orders = _dbContext.Orders
           .Include(o => o.OrderDetails)
           .ThenInclude(od => od.Product)
           .ToList();
            var order = await _dbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}

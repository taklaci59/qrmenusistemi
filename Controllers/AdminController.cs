using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qrmenusistemiuygulama18.Data;
using qrmenusistemiuygulama18.Models;
using qrmenusistemiuygulama18.Models.Enums;
using qrmenusistemiuygulama18.Services;

namespace qrmenusistemiuygulama18.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileUploadService _fileUploadService;

        public AdminController(AppDbContext context, IFileUploadService fileUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var activeOrders = await _context.Orders
                .Include(o => o.Table)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => !o.IsCompleted)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            return View(activeOrders);
        }

        // --- Ürün Yönetimi ---
        public async Task<IActionResult> Products()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product, IFormFile? imageFile)
        {
            if (imageFile != null)
            {
                product.ImageUrl = await _fileUploadService.UploadImage(imageFile);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Products");
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product, IFormFile? imageFile)
        {
            var existingProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == product.Id);
            if (existingProduct == null) return NotFound();

            if (imageFile != null)
            {
                if (!string.IsNullOrEmpty(existingProduct.ImageUrl))
                {
                    _fileUploadService.DeleteImage(existingProduct.ImageUrl);
                }
                product.ImageUrl = await _fileUploadService.UploadImage(imageFile);
            }
            else
            {
                product.ImageUrl = existingProduct.ImageUrl;
            }

            _context.Update(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Products");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    _fileUploadService.DeleteImage(product.ImageUrl);
                }
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Products");
        }

        // --- Kategori Yönetimi ---
        public async Task<IActionResult> Categories()
        {
            return View(await _context.Categories.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                _context.Categories.Add(new Category { Name = name });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Categories");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Categories");
        }

        // --- Sipariş Yönetimi ---
        public async Task<IActionResult> Orders()
        {
            var orders = await _context.Orders
                .Include(o => o.Table)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = status;
                if (status == OrderStatus.Served || status == OrderStatus.Cancelled)
                {
                    order.IsCompleted = true;
                }
                else
                {
                    order.IsCompleted = false;
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Dashboard");
        }
    }
}

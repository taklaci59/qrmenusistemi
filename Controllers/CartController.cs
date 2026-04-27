using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qrmenusistemiuygulama18.Data;
using qrmenusistemiuygulama18.Models;
using qrmenusistemiuygulama18.Services;

namespace qrmenusistemiuygulama18.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public CartController(ICartService cartService, AppDbContext context, IConfiguration configuration)
        {
            _cartService = cartService;
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var table = HttpContext.Session.GetInt32("VerifiedTable");
            if (table == null) return RedirectToAction("Menu", "Home");

            ViewBag.TableNumber = table.Value;
            var items = _cartService.GetCartItems();
            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int productId, int quantity)
        {
            var table = HttpContext.Session.GetInt32("VerifiedTable");
            if (table == null) return RedirectToAction("Menu", "Home");

            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _cartService.AddItem(product, quantity);
            }

            var secretKey = _configuration["QrSecretKey"] ?? "";
            var token = UrlSigner.GenerateToken(table.Value, secretKey);

            return RedirectToAction("Menu", "Home", new { table = table.Value, token = token });
        }

        [HttpPost]
        public IActionResult Remove(int productId)
        {
            var table = HttpContext.Session.GetInt32("VerifiedTable");
            if (table == null) return RedirectToAction("Menu", "Home");

            _cartService.RemoveItem(productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var table = HttpContext.Session.GetInt32("VerifiedTable");
            if (table == null)
            {
                ViewBag.Error = "Oturumunuzun süresi dolmuş veya masa doğrulanmamış. Lütfen QR kodu tekrar okutun.";
                return View("Error_Public");
            }

            var cartItems = _cartService.GetCartItems();
            if (cartItems.Count == 0)
            {
                var secretKey = _configuration["QrSecretKey"] ?? "";
                var token = UrlSigner.GenerateToken(table.Value, secretKey);
                return RedirectToAction("Menu", "Home", new { table = table.Value, token = token });
            }

            var dbTable = await _context.Tables.FirstOrDefaultAsync(t => t.TableNumber == table.Value);
            if (dbTable == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var order = new Order
            {
                TableId = dbTable.Id,
                TotalPrice = _cartService.GetTotalPrice(),
                OrderDate = DateTime.Now,
                Status = Models.Enums.OrderStatus.New,
                IsCompleted = false
            };

            foreach (var item in cartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price
                });
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            _cartService.ClearCart();

            ViewBag.Message = "Siparişiniz başarıyla alındı!";
            return View("OrderSuccess");
        }
    }
}

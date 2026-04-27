using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qrmenusistemiuygulama18.Data;
using qrmenusistemiuygulama18.Models;
using System.Diagnostics;
using qrmenusistemiuygulama18.Services;

namespace qrmenusistemiuygulama18.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public HomeController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            // Kullanıcı ana dizine gelirse doğrudan menüye yönlendirme yerine bir bilgilendirme yapılabilir.
            // QR Kod güvenliği için table/token zorunlu olduğundan menüye gidemez.
            return View();
        }

        public async Task<IActionResult> Menu(int? table, string? token)
        {
            var secretKey = _configuration["QrSecretKey"] ?? "";
            var verifiedTable = HttpContext.Session.GetInt32("VerifiedTable");

            // Eğer URL'de masa yoksa ama session'da doğrulanmış bir masa varsa onu kullan
            if (table == null && verifiedTable != null)
            {
                table = verifiedTable;
            }

            // Eğer masa numarası session'daki ile eşleşiyorsa token kontrolünü atla
            bool isAlreadyVerified = (table != null && verifiedTable == table);

            if (!isAlreadyVerified)
            {
                // Token doğrulama (Sadece ilk girişte veya masa değişikliğinde)
                if (table == null || string.IsNullOrEmpty(token) || !UrlSigner.ValidateToken(table.Value, token, secretKey))
                {
                    ViewBag.Error = "Geçersiz veya eksik QR kod. Lütfen masanızdaki kodu tekrar okutun.";
                    return View("Error_Public");
                }

                // Doğrulanan masayı kaydet
                HttpContext.Session.SetInt32("VerifiedTable", table.Value);
                verifiedTable = table.Value;
            }

            var categories = await _context.Categories
                .Include(c => c.Products)
                .ToListAsync();

            ViewBag.TableNumber = table ?? verifiedTable;
            return View(categories);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

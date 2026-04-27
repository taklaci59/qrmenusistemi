using System.Security.Cryptography;
using System.Text;

namespace qrmenusistemiuygulama18.Services
{
    public static class UrlSigner
    {
        public static string GenerateToken(int tableNumber, string secretKey)
        {
            var data = tableNumber.ToString();
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                // URL güvenli olması için Base64String yerine daha kısa ve temiz bir hex veya kısıtlanmış base64 kullanılabilir.
                // Burada basitlik ve kısalık için ilk 10 karakteri alacağız (çakışma riski bu senaryo için ihmal edilebilir).
                return Convert.ToHexString(hash).Substring(0, 12).ToLower();
            }
        }

        public static bool ValidateToken(int tableNumber, string token, string secretKey)
        {
            if (string.IsNullOrEmpty(token)) return false;
            var expectedToken = GenerateToken(tableNumber, secretKey);
            return string.Equals(expectedToken, token, StringComparison.OrdinalIgnoreCase);
        }
    }
}

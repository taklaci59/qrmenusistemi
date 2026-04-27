using Microsoft.AspNetCore.Http;
using qrmenusistemiuygulama18.Models;
using System.Text.Json;

namespace qrmenusistemiuygulama18.Services
{
    public interface ICartService
    {
        List<CartItem> GetCartItems();
        void AddItem(Product product, int quantity);
        void RemoveItem(int productId);
        void ClearCart();
        decimal GetTotalPrice();
    }

    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartSessionKey = "UserCart";

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        public List<CartItem> GetCartItems()
        {
            var cartJson = Session.GetString(CartSessionKey);
            return cartJson == null ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(cartJson)!;
        }

        public void AddItem(Product product, int quantity)
        {
            var cart = GetCartItems();
            var existingItem = cart.FirstOrDefault(i => i.ProductId == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = quantity,
                    ImageUrl = product.ImageUrl
                });
            }

            SaveCart(cart);
        }

        public void RemoveItem(int productId)
        {
            var cart = GetCartItems();
            var item = cart.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }
        }

        public void ClearCart()
        {
            Session.Remove(CartSessionKey);
        }

        public decimal GetTotalPrice()
        {
            return GetCartItems().Sum(i => i.TotalPrice);
        }

        private void SaveCart(List<CartItem> cart)
        {
            Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
        }
    }
}

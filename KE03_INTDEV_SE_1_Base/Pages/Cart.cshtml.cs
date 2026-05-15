using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class CartModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public CartModel(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        [BindProperty]
        public string cartData { get; set; }
        public void OnGet()
        { 

        }

        public IActionResult OnPost()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };

            // JSON uit winkelwagen lezen
            var winkelwagen = JsonSerializer.Deserialize<List<Product>>(cartData, options);

            // Bestaande producten uit database ophalen
            var echteProducten = new List<Product>();

            foreach (var item in winkelwagen)
            {
                var bestaandProduct = _productRepository.GetProductById(item.Id);

                if (bestaandProduct != null)
                {
                    echteProducten.Add(bestaandProduct);
                }
            }

            // Nieuwe order maken
            Order order = new Order
            {
                CustomerId = 1,
                OrderDate = DateTime.Now,
                Products = echteProducten
            };

            // Opslaan
            _orderRepository.AddOrder(order);

            return RedirectToPage("/Index");
        }
    }
}

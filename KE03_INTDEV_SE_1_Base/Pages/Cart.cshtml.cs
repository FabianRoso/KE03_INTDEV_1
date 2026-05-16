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
        private readonly ICustomerRepository _customerRepository;

        public CartModel(IOrderRepository orderRepository, IProductRepository productRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }

        [BindProperty]
        public string cartData { get; set; }

        public IEnumerable<Customer> customers { get; set; }
        [BindProperty]
        public int SelectedCustomerId { get; set; }

        public void OnGet()
        {
            customers = _customerRepository.GetAllCustomers();
        }

        public IActionResult OnPost()
        {
            customers = _customerRepository.GetAllCustomers();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };

            // JSON uit winkelwagen lezen
            var winkelwagen = JsonSerializer.Deserialize<List<Product>>(cartData, options);

            if (winkelwagen == null || !winkelwagen.Any())
            {
                return Page();
            }


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
                CustomerId = SelectedCustomerId,
                OrderDate = DateTime.Now,
                Products = echteProducten
            };

            // Opslaan
            _orderRepository.AddOrder(order);

            return RedirectToPage("/Index");
        }
    }
}

using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public IEnumerable<Product> Products { get; set; }

        public ProductsModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public void OnGet()
        {
            Products = _productRepository.GetAllProducts();
        }
    }
}

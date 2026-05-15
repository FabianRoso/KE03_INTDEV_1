using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class ProductDetailModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public Product product { get; set; }

        public ProductDetailModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void OnGet(int productId)
        {
            product = _productRepository.GetProductById(productId);
        }
    }
}

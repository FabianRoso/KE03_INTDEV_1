using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages.OrderHistory
{
    public class OrderHistoryModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderHistoryModel(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public IEnumerable<Customer> customers { get; set; }
        [BindProperty]
        public int SelectedCustomerId { get; set; }

        public IEnumerable<Order> allOrders { get; set; }
        public IEnumerable<Order> orders { get; set; } = new List<Order>();
        public void OnGet()
        {
            customers = _customerRepository.GetAllCustomers();
        }

        public void OnPost()
        {
            customers = _customerRepository.GetAllCustomers();
            allOrders = _orderRepository.GetAllOrders();

            orders = allOrders
                .Where(o => o.CustomerId == SelectedCustomerId)
                .ToList();
        }
    }
}

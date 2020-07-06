using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Project1.Business;
using Project1.Main.Models;

namespace Project1.Main.Controllers {

    public class CustomerController : Controller {

        private readonly ICustomerRepository mCustomerRepository;

        public CustomerController (ICustomerRepository customerRepository) {
            mCustomerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult Index (string firstname, string lastname) {

            var customer = mCustomerRepository.FindByName (firstname, lastname);
            return View (customer);
        }
    }
}

using System.Linq;

using Microsoft.AspNetCore.Mvc;

using Project1.Business;
using Project1.Main.Models;

namespace Project1.Main.Controllers {

    public class CustomerController : Controller {

        public const string SESSION_KEY = "_Customer";

        private readonly ICustomerRepository mCustomerRepository;
        private readonly IStoreRepository mStoreRepository;
        private readonly ICustomerOrderRepository mCustomerOrderRepository;

        public CustomerController (ICustomerRepository customerRepository, 
                                    IStoreRepository storeRepository,
                                    ICustomerOrderRepository customerOrderRepository) {

            mCustomerRepository = customerRepository;
            mStoreRepository = storeRepository;
            mCustomerOrderRepository = customerOrderRepository;
        }

        public IActionResult Index () {
            return View (new CustomerViewModel ());
        }

        [HttpGet]
        public IActionResult Home () {

            if (!ModelState.IsValid) {
                return BadRequest ();
            }

            var customer = HttpContext.Session.Get<CustomerModel> (SESSION_KEY);

            if (customer == default) {
                return Forbid ();
            }

            var stores = mStoreRepository.FindAll;

            var storesModel = new LandingPageViewModel {

                CustomerName = customer.Name,
                LastVisitedStore = customer.LastVisited.Name,
                StoreOptions = stores.Select (s => s.Name)
            };

            return View (storesModel);
        }

        [HttpGet]
        public IActionResult Orders () {

            if (!ModelState.IsValid) {
                return BadRequest ();
            }

            var customer = HttpContext.Session.Get<CustomerModel> (SESSION_KEY);

            if (customer == default) {
                return Forbid ();
            }

            var orders = mCustomerOrderRepository.FindOrdersByCustomer (customer.Id);

            return View (orders);
        }

        [HttpPost]
        public IActionResult Home (string firstname, string lastname) {

            if (!ModelState.IsValid) {
                return Json (new { success = false, responseText = "Invalid request state" });
            }

            var customer = mCustomerRepository.FindByName (firstname, lastname);

            if (customer == default) {
                return Json (new { success = false, responseText = $"Customer '{firstname} {lastname}' does not exists!" });
            }

            HttpContext.Session.Set<CustomerModel> (SESSION_KEY, customer);

            return Json (new { success = true, responseText = "Success!" });
        }

        [HttpPost]
        public IActionResult Create (string firstname, string lastname) {

            if (!ModelState.IsValid) {
                return Json (new { success = false, responseText = "Invalid request state" });
            }

            var newCustomer = mCustomerRepository.Add (firstname, lastname);

            if (newCustomer == default) {
                return Json (new { success = false, responseText = $"Customer {firstname} {lastname} already exists!" });
            }

            return Json (new { success = true, responseText = "New user created!" });
        }
    }
}

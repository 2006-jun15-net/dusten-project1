using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project1.Business;
using Project1.Main.Models;

namespace Project1.Main.Controllers {

    /// <summary>
    /// Provides pages and requests relevant to a Customer's actions
    /// </summary>
    public class CustomerController : LoggedController {

        /// <summary>
        /// Key string to get the current CustomerModel object stored
        /// in the site's session values
        /// </summary>
        public const string SESSION_KEY = "_Customer";

        private readonly ICustomerRepository mCustomerRepository;
        private readonly IStoreRepository mStoreRepository;
        private readonly ICustomerOrderRepository mCustomerOrderRepository;

        public CustomerController (ILogger logger,
                                    ICustomerRepository customerRepository,
                                    IStoreRepository storeRepository,
                                    ICustomerOrderRepository customerOrderRepository) : base (logger) {

            mCustomerRepository = customerRepository;
            mStoreRepository = storeRepository;
            mCustomerOrderRepository = customerOrderRepository;

            mLogger.LogDebug ("CustomerController instance created");
        }

        /// <summary>
        /// Customer login page (first page of the website)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index () {

            mLogger.LogDebug ("Customer/Index request");

            return View (new CustomerViewModel {
                CustomerOptions = mCustomerRepository.FindAll
            });
        }

        /// <summary>
        /// Customer home page (sent here after login)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Home () {

            mLogger.LogDebug ("Customer/Home request");

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

        /// <summary>
        /// Page with list of all the current customer's order history
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Orders () {

            mLogger.LogDebug ("Customer/Orders request");

            var customer = HttpContext.Session.Get<CustomerModel> (SESSION_KEY);

            if (customer == default) {
                return Forbid ();
            }

            var orders = mCustomerOrderRepository.FindOrdersByCustomer (customer.Id);

            return View (new OrdersViewModel {

                CustomerOrders = orders,
                CustomerName = customer.Name
            });
        }

        /// <summary>
        /// Request to validate the customer's potential login
        /// </summary>
        /// <param name="customerView">
        /// Model containing firstname and lastname from the login 
        /// page's form data
        /// </param>
        /// <returns>JSON object with success flag and response text</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login ([Bind ("Firstname", "Lastname")] CustomerViewModel customerView) {

            mLogger.LogDebug ("Customer/Login request");

            if (!ModelState.IsValid) {
                return Json (new { success = false, responseText = "Validation error" });
            }

            var firstname = customerView.Firstname;
            var lastname = customerView.Lastname;

            var customer = mCustomerRepository.FindByName (firstname, lastname).Result;

            if (customer == default) {
                return Json (new { success = false, responseText = $"Customer '{firstname} {lastname}' does not exists!" });
            }

            HttpContext.Session.Set<CustomerModel> (SESSION_KEY, customer);

            return Json (new { success = true, responseText = "Success!" });
        }

        /// <summary>
        /// Create a new customer (only works if the customer with (firstname, lastname)
        /// Doesn't already exist in the database)
        /// </summary>
        /// <param name="customerView"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create ([Bind ("Firstname", "Lastname")] CustomerViewModel customerView) {

            mLogger.LogDebug ("Customer/Create request");

            var firstname = customerView.Firstname;
            var lastname = customerView.Lastname;

            if (!ModelState.IsValid) {
                return Json (new { success = false, responseText = "Validation error" });
            }

            var newCustomer = mCustomerRepository.Add (new CustomerModel {
                Name = firstname + " " + lastname    
            });

            if (newCustomer == default) {
                return Json (new { success = false, responseText = $"Customer {firstname} {lastname} already exists!" });
            }

            return Json (new { success = true, responseText = "New user created!" });
        }
    }
}

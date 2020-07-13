using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project1.Business;
using Project1.Main.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Main.Controllers {

    /// <summary>
    /// Provides pages and requests relevant to a Customer's actions
    /// </summary>
    public class CustomerController : Controller {

        /// <summary>
        /// Key string to get the current CustomerModel object stored
        /// in the site's session values
        /// </summary>
        public const string SESSION_KEY = "_Customer";

        private readonly ICustomerRepository mCustomerRepository;
        private readonly IStoreRepository mStoreRepository;
        private readonly ICustomerOrderRepository mCustomerOrderRepository;
        private readonly ILogger<CustomerController> mLogger;

        public CustomerController (ICustomerRepository customerRepository,
                                    IStoreRepository storeRepository,
                                    ICustomerOrderRepository customerOrderRepository,
                                    ILogger<CustomerController> logger) {

            mCustomerRepository = customerRepository;
            mStoreRepository = storeRepository;
            mCustomerOrderRepository = customerOrderRepository;
            mLogger = logger;

            mLogger.LogInformation ("CustomerController instance created");
        }

        /// <summary>
        /// Customer login page (first page of the website)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index () {

            mLogger.LogInformation ("Customer/Index request");

            var customers = await mCustomerRepository.FindAllAsync ();

            return View (new CustomerViewModel {
                CustomerOptions = customers
            });
        }

        /// <summary>
        /// Customer home page (sent here after login)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Home () {

            mLogger.LogInformation ("Customer/Home request");

            var customer = HttpContext.Session.Get<CustomerModel> (SESSION_KEY);

            if (customer == default) {
                return Forbid ();
            }

            var stores = await mStoreRepository.FindAllAsync ();

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
        public async Task<IActionResult> Orders () {

            mLogger.LogInformation ("Customer/Orders request");

            var customer = HttpContext.Session.Get<CustomerModel> (SESSION_KEY);

            if (customer == default) {
                return Forbid ();
            }

            var orders = await mCustomerOrderRepository.FindOrdersByCustomerAsync (customer.Id);

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
        public async Task<IActionResult> Login ([Bind ("Firstname", "Lastname")] CustomerViewModel customerView) {

            mLogger.LogInformation ("Customer/Login request");

            if (!ModelState.IsValid) {
                return Json (new { success = false, responseText = "Validation error" });
            }

            var firstname = customerView.Firstname;
            var lastname = customerView.Lastname;

            var customer = await mCustomerRepository.FindByNameAsync (firstname, lastname);

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
        public async Task<IActionResult> Create ([Bind ("Firstname", "Lastname")] CustomerViewModel customerView) {

            mLogger.LogInformation ("Customer/Create request");

            var firstname = customerView.Firstname;
            var lastname = customerView.Lastname;

            if (!ModelState.IsValid) {
                return Json (new { success = false, responseText = "Validation error" });
            }

            var newCustomer = await mCustomerRepository.AddAsync (new CustomerModel {
                Name = firstname + " " + lastname
            });

            if (newCustomer == false) {
                return Json (new { success = false, responseText = $"Customer {firstname} {lastname} already exists!" });
            }

            return Json (new { success = true, responseText = "New user created!" });
        }
    }
}

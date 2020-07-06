using System.Linq;

using Microsoft.AspNetCore.Mvc;

using Project1.Business;
using Project1.Main.Models;

namespace Project1.Main.Controllers {

    public class CustomerController : Controller {

        private readonly ICustomerRepository mCustomerRepository;
        private readonly IStoreRepository mStoreRepository;

        public CustomerController (ICustomerRepository customerRepository, IStoreRepository storeRepository) {

            mCustomerRepository = customerRepository;
            mStoreRepository = storeRepository;
        }

        public IActionResult Index () {
            return View (new CustomerViewModel ());
        }

        [HttpGet]
        public IActionResult Home (CustomerViewModel customerModel) {

            var customer = mCustomerRepository.FindByName (
                            customerModel.Firstname, customerModel.Lastname);

            var stores = mStoreRepository.FindAll;

            var storesModel = new StoresViewModel {

                LastVisitedStore = customer.LastVisited.Name,
                StoreOptions = stores.Select (s => s.Name)
            };

            return View (storesModel);
        }
    }
}

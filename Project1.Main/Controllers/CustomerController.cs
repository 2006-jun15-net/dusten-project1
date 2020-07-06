using System.Linq;
using System.Threading.Tasks;

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

        public IActionResult Index (string firstname, string lastname) {

            var customer = mCustomerRepository.FindByName (firstname, lastname);
            var stores = mStoreRepository.FindAll;

            return View (new CustomerViewModel {

                LastVisitedStore = customer.LastVisited.Name,
                StoreOptions = stores.Select ( s => s.Name)
            });
        }
    }
}

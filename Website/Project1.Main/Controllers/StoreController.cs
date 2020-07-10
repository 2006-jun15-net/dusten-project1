using Microsoft.AspNetCore.Mvc;

using Project1.Business;
using Project1.Main.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace Project1.Main.Controllers {

    public class StoreController : Controller {

        private readonly IStoreRepository mStoreRepository;
        private readonly ICustomerOrderRepository mOrderRepository;

        public StoreController (IStoreRepository storeRepository, ICustomerOrderRepository customerOrderRepository) {

            mStoreRepository = storeRepository;
            mOrderRepository = customerOrderRepository;
        }

        [HttpGet]
        public IActionResult Index (string name) {

            var store = mStoreRepository.FindByName (name);

            if (store == default) {
                return NotFound ();
            }

            var customer = HttpContext.Session.Get<CustomerModel> (CustomerController.SESSION_KEY);

            return View (new StoreViewModel {

                Name = store.Name,
                Stock = store.StoreStock,
                HasCustomer = customer != default
            });
        }

        [HttpGet]
        public IActionResult ListOrders (string name) {

            var customer = HttpContext.Session.Get<CustomerModel> (CustomerController.SESSION_KEY);

            if (customer == default) {
                return Forbid ();
            }

            var store = mStoreRepository.FindByName (name);

            if (store == default) {
                return NotFound ();
            }

            var orders = mOrderRepository.FindOrdersByCustomerAndStore (customer.Id, store.Id);

            return View ("~/Views/Store/Orders/List.cshtml", new OrdersViewModel {

                CustomerOrders = orders,
                CustomerName = customer.Name,
                StoreName = store.Name
            });
        }

        [HttpGet]
        public IActionResult NewOrder (string name) {

            var customer = HttpContext.Session.Get<CustomerModel> (CustomerController.SESSION_KEY);

            if (customer == default) {
                return Forbid ();
            }

            var store = mStoreRepository.FindByName (name);

            if (store == default) {
                return NotFound ();
            }

            return View ("~/Views/Store/Orders/New.cshtml", new StoreViewModel {

                Name = store.Name,
                Stock = store.StoreStock
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrder (List<OrderLineModel> lines) {

            Debug.WriteLine (lines.Count);

            return Json (new { success = true, responseText = "Success" });
        }
    }
}

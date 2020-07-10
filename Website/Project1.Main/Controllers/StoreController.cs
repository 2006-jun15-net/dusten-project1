using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project1.Business;
using Project1.Main.Models;
using System;
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
        public IActionResult CreateOrder (List<OrderLineModel> lines, string storeName) {

            Debug.WriteLine (lines.Count);

            if (!ModelState.IsValid) {
                return Json (new { success = false, responseText = "Invalid data sent" });
            }

            var customer = HttpContext.Session.Get<CustomerModel> (CustomerController.SESSION_KEY);

            if (customer == default) {
                return Json (new { success = false, responseText = "Access denied, not logged in"});
            }

            var store = mStoreRepository.FindByName (storeName);

            if (store == default) {
                return Json (new { success = false, responseText = $"Store '{storeName}' does not exist"});
            }

            bool orderSuccess = mOrderRepository.Add (new CustomerOrderModel {

                OrderLine = lines,
                Timestamp = DateTime.Now

            }, customer.Id, store.Id);

            if (!orderSuccess) {
                return Json (new { success = false, responseText = "Failed to place order (invalid data)"});
            }

            return Json (new { success = true, responseText = "Success" });
        }
    }
}

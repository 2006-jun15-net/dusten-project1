﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Project1.Business;
using Project1.Main.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Main.Controllers {

    public class StoreController : LoggedController {

        private readonly IStoreRepository mStoreRepository;
        private readonly ICustomerOrderRepository mOrderRepository;

        public StoreController (ILogger logger,
                                IStoreRepository storeRepository, 
                                ICustomerOrderRepository customerOrderRepository) : base (logger) {

            mStoreRepository = storeRepository;
            mOrderRepository = customerOrderRepository;
        }

        [HttpGet]
        public IActionResult Index (string name) {

            var store = mStoreRepository.FindByNameAsync (name).Result;

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
        public async Task<IActionResult> ListOrders (string name) {

            var customer = HttpContext.Session.Get<CustomerModel> (CustomerController.SESSION_KEY);

            if (customer == default) {
                return Forbid ();
            }

            var store = await mStoreRepository.FindByNameAsync (name);

            if (store == default) {
                return NotFound ();
            }

            var orders = await mOrderRepository.FindOrdersByCustomerAndStoreAsync (customer.Id, store.Id);

            return View ("~/Views/Store/Orders/List.cshtml", new OrdersViewModel {

                CustomerOrders = orders,
                CustomerName = customer.Name,
                StoreName = store.Name
            });
        }

        [HttpGet]
        public async Task<IActionResult> NewOrder (string name) {

            var customer = HttpContext.Session.Get<CustomerModel> (CustomerController.SESSION_KEY);

            if (customer == default) {
                return Forbid ();
            }

            var store = await mStoreRepository.FindByNameAsync (name);

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
        public async Task<IActionResult> CreateOrder (List<OrderLineModel> lines, string storeName) {

            if (!ModelState.IsValid) {
                return Json (new { success = false, responseText = "Invalid data sent" });
            }

            var customer = HttpContext.Session.Get<CustomerModel> (CustomerController.SESSION_KEY);

            if (customer == default) {
                return Json (new { success = false, responseText = "Access denied, not logged in"});
            }

            var store = await mStoreRepository.FindByNameAsync (storeName);

            if (store == default) {
                return Json (new { success = false, responseText = $"Store '{storeName}' does not exist"});
            }

            var orderLines = lines.Where (l => l.ProductQuantity > 0);

            if (orderLines.Count() == 0) {
                return Json (new { success = false, responseText = "Order contains no items"});
            }

            var orderModel = new CustomerOrderModel {

                OrderLine = orderLines, 
                Timestamp = DateTime.Now
            };

            if (orderModel.ProductCount > CustomerOrderModel.MAX_PRODUCTS) {
                return Json (new { success = false, responseText = "Order exceeds maximum quantity"});
            }

            bool orderSuccess = await mOrderRepository.AddAsync (orderModel, customer.Id, store.Id);

            if (!orderSuccess) {
                return Json (new { success = false, responseText = "Failed to place order (invalid data)"});
            }

            return Json (new { success = true, responseText = "Success" });
        }
    }
}

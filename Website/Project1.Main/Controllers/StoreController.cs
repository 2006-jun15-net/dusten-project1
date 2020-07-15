using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Project1.Business;
using Project1.Main.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Main.Controllers {

    public class StoreController : Controller {

        private readonly IStoreRepository mStoreRepository;
        private readonly ICustomerOrderRepository mOrderRepository;
        private readonly IStoreStockRepository mStoreStockRepository;
        private readonly ILogger<StoreController> mLogger;

        public StoreController (IStoreRepository storeRepository,
                                ICustomerOrderRepository customerOrderRepository,
                                IStoreStockRepository storeStockRepository,
                                ILogger<StoreController> logger) {

            mStoreRepository = storeRepository;
            mOrderRepository = customerOrderRepository;
            mStoreStockRepository = storeStockRepository;
            mLogger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index (string name) {

            if (mLogger != null) {
                mLogger.LogInformation ("Store/Index request");
            }

            var store = await mStoreRepository.FindByNameAsync (name);

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

            if (mLogger != null) {
                mLogger.LogInformation ("Store/ListOrders request");
            }

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

            if (mLogger != null) {
                mLogger.LogInformation ("Store/NewOrder request");
            }

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

            if (mLogger != null) {
                mLogger.LogInformation ("Store/CreateOrder request");
            }

            if (!ModelState.IsValid) {
                return Json (new JsonResponse(false, "Invalid data sent"));
            }

            var customer = HttpContext.Session.Get<CustomerModel> (CustomerController.SESSION_KEY);

            if (customer == default || customer.Id == null) {
                return Json (new JsonResponse(false, "Access denied, not logged in"));
            }

            var store = await mStoreRepository.FindByNameAsync (storeName);

            if (store == default) {
                return Json (new JsonResponse(false, $"Store '{storeName}' does not exist"));
            }

            var orderLines = lines.Where (l => l.ProductQuantity > 0);

            if (!orderLines.Any ()) {
                return Json (new JsonResponse(false, "Order contains no items"));
            }

            var orderModel = new CustomerOrderModel {

                CustomerId = Convert.ToInt32 (customer.Id),
                StoreId = store.Id,

                Timestamp = DateTime.Now,
                OrderLine = orderLines
            };

            if (orderModel.ProductCount > CustomerOrderModel.MAX_PRODUCTS) {
                return Json (new JsonResponse(false, "Order exceeds maximum quantity"));
            }

            foreach (var line in orderLines) {

                if (!await mStoreStockRepository.RemoveQuantityAsync (line.Product.Name, store.Id, line.ProductQuantity)) {
                    return Json (new JsonResponse (false, $"Quantity of product {line.Product.Name} exceeds available quantity"));
                }
            }
            await mOrderRepository.AddAsync (orderModel);

            return Json (JsonResponse.Success);
        }
    }
}

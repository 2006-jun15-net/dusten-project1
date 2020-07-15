using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;
using Moq;

using Project1.Business;
using Project1.DataAccess.Repository;
using Project1.Main.Controllers;
using Microsoft.AspNetCore.Mvc;
using Project1.Main.Models;
using Microsoft.AspNetCore.Http;
using Project1.Main;

namespace Project1.Test.Main.Controllers {

    public class StoreControllerTest {

        private readonly StoreController mStoreController;

        public StoreControllerTest () {

            var mockStoreRepo = new Mock<StoreRepository> ();
            var mockOrderRepo = new Mock<CustomerOrderRepository> ();
            var mockStockRepo = new Mock<StoreStockRepository> ();
            var mockHttpContext = new Mock<HttpContext> ();

            List<StoreModel> stores = new List<StoreModel> () {

                new StoreModel {

                    Id = 1,
                    Name = "Test",
                    StoreStock = new List<StoreStockModel> {

                        new StoreStockModel {

                            ProductQuantity = 2,
                            Product = new ProductModel {
                                Name = "Test Product"
                            }
                        }
                    }
                }
            };

            List<CustomerOrderModel> orders = new List<CustomerOrderModel> () {

                new CustomerOrderModel {

                    CustomerId = 1,
                    StoreId = 1
                }
            };

            // Find all (store)
            mockStoreRepo.Setup (
                repo => repo.FindAllAsync ()
            ).Returns (
                async () => await Task.Run (() => stores)
            );

            // Find by name (store)
            mockStoreRepo.Setup (
                repo => repo.FindByNameAsync (It.IsAny<string> ())
            ).Returns (
                async (string name) =>
                await Task.Run (() => stores.Where (s => s.Name == name).FirstOrDefault ())
            );

            // Add (order)
            mockOrderRepo.Setup (
                repo => repo.AddAsync (It.IsAny<CustomerOrderModel> ())
            ).Returns (
                async (CustomerOrderModel customerOrder) =>
                await Task.Run (() => orders.Add (customerOrder))
            );

            // Find by customer (order)
            mockOrderRepo.Setup (
                repo => repo.FindOrdersByCustomerAsync (It.IsAny<int> ())
            ).Returns (
                async (int customerId) =>
                await Task.Run (() => orders.Where (o => o.CustomerId == customerId))
            );

            // Find by customer and store (order)
            mockOrderRepo.Setup (
                repo => repo.FindOrdersByCustomerAndStoreAsync (It.IsAny<int> (), It.IsAny<int> ())
            ).Returns (
                async (int customerId, int storeId) =>
                await Task.Run (() => orders.Where (o => o.CustomerId == customerId && o.StoreId == storeId))
            );

            mockStockRepo.Setup (
                repo => repo.RemoveQuantityAsync (It.IsAny<string> (), It.IsAny<int> (), It.IsAny<int> ())
            ).Returns (
                async (string productName, int storeId, int quantity) =>
                await Task.Run (() => {

                    var storeStock = stores.Where (s => s.Id == storeId).Select (s => s.StoreStock).First ();
                    storeStock.Where (s => s.Product.Name == productName).First ().ProductQuantity -= quantity;

                    return true;
                })
            );

            mockStoreRepo.SetupAllProperties ();
            mockOrderRepo.SetupAllProperties ();
            mockStockRepo.SetupAllProperties ();

            mStoreController = new StoreController (mockStoreRepo.Object, mockOrderRepo.Object, mockStockRepo.Object, null);

            var mockSession = new MockHttpSession ();
            mockSession[CustomerController.SESSION_KEY] = "{\"Id\":1,\"Name\":\"Test Customer\",\"LastVisited\":{\"Id\":0,\"Name\":null,\"StoreStock\":null}}";

            mockHttpContext.Setup (
                s => s.Session
            ).Returns (
                mockSession
            );

            mStoreController.ControllerContext.HttpContext = mockHttpContext.Object;
        }

        [Fact]
        public async void TestIndex () {

            ViewResult request = (ViewResult)(await mStoreController.Index ("Test"));

            StoreViewModel model = (StoreViewModel)request.Model;

            Assert.Equal ("Test", model.Name);
            Assert.True (model.HasCustomer);
            Assert.Single (model.Stock);
        }

        [Fact]
        public async void TestIndexFail () {

            NotFoundResult request = (NotFoundResult)(await mStoreController.Index ("Not a store"));

            Assert.Equal (mStoreController.NotFound ().StatusCode, request.StatusCode);
        }

        [Fact]
        public async void TestListOrders () {

            ViewResult request = (ViewResult)(await mStoreController.ListOrders ("Test"));

            var model = (OrdersViewModel)request.Model;

            Assert.Equal ("Test", model.StoreName);
            Assert.Equal ("Test Customer", model.CustomerName);
        }

        [Fact]
        public async void TestListOrdersFail () {

            NotFoundResult request = (NotFoundResult)(await mStoreController.ListOrders ("Not a store"));

            Assert.Equal (mStoreController.NotFound ().StatusCode, request.StatusCode);
        }

        [Fact]
        public async void TestNewOrder () {

            ViewResult request = (ViewResult)(await mStoreController.NewOrder ("Test"));

            var model = (StoreViewModel)request.Model;

            Assert.Equal ("Test", model.Name);
        }

        [Fact]
        public async void TestNewOrderFail () {

            NotFoundResult request = (NotFoundResult)(await mStoreController.NewOrder ("Not a store"));

            Assert.Equal (mStoreController.NotFound ().StatusCode, request.StatusCode);
        }

        [Fact]
        public async void TestCreateOrder () {

            var orderLines = new List<OrderLineModel> {

                new OrderLineModel {

                    ProductQuantity = 1,

                    Product = new ProductModel {
                        Name = "Test Product"
                    }
                }
            };

            JsonResult response = (JsonResult)(await mStoreController.CreateOrder (orderLines, "Test"));

            var jsonResponse = (JsonResponse)response.Value;

            Assert.Same (JsonResponse.Success, jsonResponse);
        }

        [Fact]
        public async void TestCreateOrderFail () {

            JsonResult response = (JsonResult)(await mStoreController.CreateOrder (new List<OrderLineModel> (), "Not a store"));

            var jsonResponse = (JsonResponse)response.Value;

            Assert.False (jsonResponse.SuccessFlag);
            Assert.Equal ("Store 'Not a store' does not exist", jsonResponse.ResponseText);
        }

        [Fact]
        public async void TestCreateOrderNoItems () {

            var orderLines = new List<OrderLineModel> {

                new OrderLineModel {
                    ProductQuantity = 0
                }
            };

            JsonResult response = (JsonResult)(await mStoreController.CreateOrder (orderLines, "Test"));

            var jsonResponse = (JsonResponse)response.Value;

            Assert.False (jsonResponse.SuccessFlag);
            Assert.Equal ("Order contains no items", jsonResponse.ResponseText);
        }

        [Fact]
        public async void TestCreateOrderTooManyItems () {

            var orderLines = new List<OrderLineModel> {

                new OrderLineModel {
                    ProductQuantity = 21
                }
            };

            JsonResult response = (JsonResult)(await mStoreController.CreateOrder (orderLines, "Test"));

            var jsonResponse = (JsonResponse)response.Value;

            Assert.False (jsonResponse.SuccessFlag);
            Assert.Equal ("Order exceeds maximum quantity", jsonResponse.ResponseText);
        }
    }
}

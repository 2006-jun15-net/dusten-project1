using Microsoft.EntityFrameworkCore;
using Project1.DataAccess.Model;
using Project1.DataAccess.Repository;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Project1.Test.DataAccess.Repository {

    public class CustomerOrderRepositoryTest {

        private readonly Project0Context mContext;

        public CustomerOrderRepositoryTest () {

            mContext = new Project0Context (new DbContextOptionsBuilder<Project0Context> ()
                .UseInMemoryDatabase (databaseName: "CustomerOrderDatabase")
                .Options);

            mContext.Database.EnsureDeleted ();

            mContext.CustomerOrder.Add (new CustomerOrder {

                CustomerId = 1,
                StoreId = 1,
                
                Customer = new Customer {
                    Id = 1
                },
                Store = new Store {
                    Id = 1
                },
                OrderLine = new List<OrderLine> {
                    new OrderLine {
                        Product = new Product ()
                    }
                }
            });
            mContext.SaveChanges ();
        }

        [Fact]
        public void TestFindOrdersByCustomer () {

            var customerOrderRepository = new CustomerOrderRepository (null, mContext);
            var ordersByCustomer = customerOrderRepository.FindOrdersByCustomerAsync (1).Result;

            Assert.Single (ordersByCustomer);
            Assert.Equal (1, ordersByCustomer.First ().CustomerId);
        }

        [Fact]
        public void TestFindOrdersByCustomerFail () {

            var customerOrderRepository = new CustomerOrderRepository (null, mContext);
            var ordersByCustomer = customerOrderRepository.FindOrdersByCustomerAsync (-1).Result;

            Assert.Empty (ordersByCustomer);
        }

        [Fact]
        public void TestFindOrdersByCustomerAndStore () {

            var customerOrderRepository = new CustomerOrderRepository (null, mContext);
            var ordersByCustomerAndStore =
                customerOrderRepository.FindOrdersByCustomerAndStoreAsync (1, 1).Result;

            Assert.Single (ordersByCustomerAndStore);
            Assert.Equal (1, ordersByCustomerAndStore.First ().CustomerId);
            Assert.Equal (1, ordersByCustomerAndStore.First ().StoreId);
        }

        [Fact]
        public void TestFindOrdersByCustomerAndStoreFail () {

            var customerOrderRepository = new CustomerOrderRepository (null, mContext);
            var ordersByCustomerAndStore =
                customerOrderRepository.FindOrdersByCustomerAndStoreAsync (-1, -1).Result;

            Assert.Empty (ordersByCustomerAndStore);
        }
    }
}

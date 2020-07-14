using Microsoft.EntityFrameworkCore;
using Project1.Business;
using Project1.DataAccess.Model;
using Project1.DataAccess.Repository;

using Xunit;

namespace Project1.Test.DataAccess.Repository {

    public class CustomerRepositoryTest {

        private readonly Project0Context mContext;

        public CustomerRepositoryTest () {

            mContext = new Project0Context (new DbContextOptionsBuilder<Project0Context> ()
                .UseInMemoryDatabase (databaseName: "CustomerDatabase")
                .Options);

            mContext.Database.EnsureDeleted ();

            mContext.Customer.Add (new Customer {

                Firstname = "Test",
                Lastname = "One"
            });
            mContext.SaveChanges ();
        }

        [Fact]
        public void TestFindAll () {

            var customerRepository = new CustomerRepository (null, mContext);
            var allCustomers = customerRepository.FindAllAsync ().Result;

            Assert.Single (allCustomers);
        }

        [Fact]
        public void TestFindByNameSuccess () {

            var customerRepository = new CustomerRepository (null, mContext);
            var customerByName = customerRepository.FindByNameAsync ("Test", "One").Result;

            Assert.NotSame (default (CustomerModel), customerByName);
            Assert.Equal ("Test One", customerByName.Name);
        }

        [Fact]
        public void TestFindByNameFail () {

            var customerRepository = new CustomerRepository (null, mContext);
            var customerByName = customerRepository.FindByNameAsync ("Test", "Two").Result;

            Assert.Same (default (CustomerModel), customerByName);
        }
    }
}

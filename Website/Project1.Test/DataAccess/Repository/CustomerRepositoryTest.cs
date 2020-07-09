using System.Linq;
using System.Collections.Generic;

using Xunit;
using Moq;

using Project1.DataAccess.Repository;
using Project1.Business;

namespace Project1.Test.DataAccess.Repository {

    public class CustomerRepositoryTest {

        private readonly CustomerRepository mCustomerRepository;

        public CustomerRepositoryTest () {

            var mockCustomerRepo = new Mock<CustomerRepository> ();

            List<CustomerModel> customers = new List<CustomerModel> () {

                new CustomerModel {
                    Name = "Test One"
                },

                new CustomerModel {
                    Name = "Test Two"
                }
            };
            
            mockCustomerRepo.Setup (
                repo => repo.FindByName (It.IsAny<string> (), It.IsAny<string> ())  
            ).Returns (
                (string firstname, string lastname) => customers.Where (c => c.Name == firstname + " " + lastname).FirstOrDefault ()
            );

            mCustomerRepository = mockCustomerRepo.Object;
        }

        [Fact]
        public void TestFindByNameSuccess () {

            var customerByName = mCustomerRepository.FindByName ("Test", "One");

            Assert.NotSame (default(CustomerModel), customerByName);
            Assert.Equal ("Test One", customerByName.Name);
        }

        [Fact]
        public void TestFindByNameFail () {

            var customerByName = mCustomerRepository.FindByName ("Test", "Three");
            Assert.Same (default(CustomerModel), customerByName);
        }
    }
}

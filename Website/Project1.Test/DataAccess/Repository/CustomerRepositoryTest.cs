using System.Linq;
using System.Collections.Generic;

using Xunit;
using Moq;

using Project1.DataAccess.Repository;
using Project1.Business;
using System.Threading.Tasks;

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

            // Find all
            mockCustomerRepo.Setup (
                repo => repo.FindAllAsync ()   
            ).Returns (
                async () => await Task.Run (() => customers)  
            );
            
            // Find by name
            mockCustomerRepo.Setup (
                repo => repo.FindByNameAsync (It.IsAny<string> (), It.IsAny<string> ())  
            ).Returns (
                async (string firstname, string lastname) => 
                await Task.Run (() => customers.Where (c => c.Name == firstname + " " + lastname).FirstOrDefault ())
            );

            mCustomerRepository = mockCustomerRepo.Object;
        }

        [Fact]
        public async void TestFindAll () {

            var allCustomers = await mCustomerRepository.FindAllAsync ();

            Assert.Equal (2, allCustomers.Count ());
        }

        [Fact]
        public async void TestFindByNameSuccess () {

            var customerByName = await mCustomerRepository.FindByNameAsync ("Test", "One");

            Assert.NotSame (default(CustomerModel), customerByName);
            Assert.Equal ("Test One", customerByName.Name);
        }

        [Fact]
        public async void TestFindByNameFail () {

            var customerByName = await mCustomerRepository.FindByNameAsync ("Test", "Three");

            Assert.Same (default(CustomerModel), customerByName);
        }
    }
}

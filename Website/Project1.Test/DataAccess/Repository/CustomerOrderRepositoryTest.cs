using System.Linq;
using System.Collections.Generic;

using Xunit;
using Moq;

using Project1.DataAccess.Repository;
using Project1.Business;
using System.Threading.Tasks;

namespace Project1.Test.DataAccess.Repository {

    public class CustomerOrderRepositoryTest {

        private readonly CustomerOrderRepository mCustomerOrderRepository;

        public CustomerOrderRepositoryTest () {

            var mockOrderRepo = new Mock<CustomerOrderRepository> ();

            List<CustomerOrderModel> orders = new List<CustomerOrderModel> () {

                new CustomerOrderModel {

                },

                new CustomerOrderModel {

                }
            };

            mCustomerOrderRepository = mockOrderRepo.Object;
        }
    }
}

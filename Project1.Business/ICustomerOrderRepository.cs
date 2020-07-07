using System.Collections.Generic;

namespace Project1.Business {

    public interface ICustomerOrderRepository {

        IEnumerable<CustomerOrderModel> FindOrdersByCustomer (int? customerId);
    }
}

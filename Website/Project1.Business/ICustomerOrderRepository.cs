using System.Collections.Generic;

namespace Project1.Business {

    /// <summary>
    /// Customer order repository interface
    /// </summary>
    public interface ICustomerOrderRepository {

        IEnumerable<CustomerOrderModel> FindOrdersByCustomer (int? customerId);
        IEnumerable<CustomerOrderModel> FindOrdersByCustomerAndStore (int? customerId, int storeId);
        bool Add (CustomerOrderModel order, int? customerId, int storeId);   
    }
}

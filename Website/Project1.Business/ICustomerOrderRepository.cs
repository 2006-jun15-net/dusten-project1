using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project1.Business {

    /// <summary>
    /// Customer order repository interface
    /// </summary>
    public interface ICustomerOrderRepository {

        Task AddAsync (CustomerOrderModel order);
        Task<IEnumerable<CustomerOrderModel>> FindOrdersByCustomerAsync (int? customerId);
        Task<IEnumerable<CustomerOrderModel>> FindOrdersByCustomerAndStoreAsync (int? customerId, int storeId);
    }
}

using Project1.Business;

using System.Collections.Generic;

namespace Project1.Main.Models {

    public class OrdersViewModel {

        public IEnumerable<CustomerOrderModel> CustomerOrders { get; set; }
        public string CustomerName { get; set; }
    }
}

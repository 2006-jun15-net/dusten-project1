using Microsoft.EntityFrameworkCore;
using Project1.Business;

using System.Collections.Generic;
using System.Linq;

namespace Project1.DataAccess.Repository {

    public class CustomerOrderRepository : Repository, ICustomerOrderRepository {

        public CustomerOrderRepository () : base () { }

        public virtual IEnumerable<CustomerOrderModel> FindOrdersByCustomer (int? customerId) {

            if (customerId == null) {
                return new List<CustomerOrderModel> ();
            }

            var ordersByCustomer = mContext.CustomerOrder.Where (c => c.CustomerId == customerId)
                .Include (c => c.Customer).Include (c => c.Store).Include (c => c.OrderLine).ThenInclude (o => o.Product);

            return ordersByCustomer.Select (c => new CustomerOrderModel {

                OrderNumber = c.Id,
                Timestamp = c.Timestamp,
                StoreName = c.Store.Name,
                CustomerName = c.Customer.Firstname + " " + c.Customer.Lastname,

                OrderLine = c.OrderLine.Select (o => new OrderLineModel {

                    ProductQuantity = o.ProductQuantity,

                    Product = new ProductModel {

                        Price = o.Product.Price,
                        Name = o.Product.Name
                    }
                })
            });
        }
    }
}

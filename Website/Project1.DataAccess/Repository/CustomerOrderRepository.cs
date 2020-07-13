using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Project1.Business;
using Project1.DataAccess.Model;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.DataAccess.Repository {

    /// <summary>
    /// Repository to find and create customer orders
    /// </summary>
    public class CustomerOrderRepository : Repository, ICustomerOrderRepository {

        public CustomerOrderRepository (ILogger logger, Project0Context context) : base (logger, context) { }

        /// <summary>
        /// FOR UNIT TESTS ONLY!!!!
        /// </summary>
        public CustomerOrderRepository () { }

        /// <summary>
        /// Create a new order for a given customer at a given store
        /// </summary>
        /// <param name="order"></param>
        /// <param name="customerId"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public virtual async Task<bool> AddAsync (CustomerOrderModel order, int? customerId, int storeId) {

            if (customerId == null) {
                return false;
            }

            var added = mContext.CustomerOrder.Add (new CustomerOrder {

                CustomerId = (int)customerId,
                StoreId = storeId,

                Timestamp = order.Timestamp,

                OrderLine = order.OrderLine.Select (o => new OrderLine {

                    ProductId = o.Product.Id,
                    ProductQuantity = o.ProductQuantity

                }).ToList ()
            });

            mLogger.LogDebug (added.ToString ());

            await mContext.SaveChangesAsync ();

            return true;
        }

        /// <summary>
        /// Find all orders for a given customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<CustomerOrderModel>> FindOrdersByCustomerAsync (int? customerId) {

            if (customerId == null) {
                return new List<CustomerOrderModel> ();
            }

            var ordersByCustomer = mContext.CustomerOrder.Where (c => c.CustomerId == customerId)
                .Include (c => c.Customer).Include (c => c.Store).Include (c => c.OrderLine).ThenInclude (o => o.Product);

            IQueryable<CustomerOrderModel> selection = ordersByCustomer.Select (c => new CustomerOrderModel {

                OrderNumber = c.Id,
                Timestamp = c.Timestamp,
                StoreName = c.Store.Name,
                CustomerName = c.Customer.Firstname + " " + c.Customer.Lastname,

                OrderLine = c.OrderLine.Select (o => new OrderLineModel {

                    ProductQuantity = o.ProductQuantity,

                    Product = new ProductModel {

                        Price = o.Product.Price,
                        Name = o.Product.Name,
                        Id = o.Product.Id
                    }
                })
            });

            mLogger.LogDebug (selection.ToString ());

            return await selection.ToListAsync ();
        }

        /// <summary>
        /// Find all orders for a given customer at a given store
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<CustomerOrderModel>> FindOrdersByCustomerAndStoreAsync (int? customerId, int storeId) {

            if (customerId == null) {
                return new List<CustomerOrderModel> ();
            }

            var ordersByCustomerAndStore = mContext.CustomerOrder.Where (c => c.CustomerId == customerId && c.StoreId == storeId)
                .Include (c => c.Customer).Include (c => c.Store).Include (c => c.OrderLine).ThenInclude (o => o.Product);

            IQueryable<CustomerOrderModel> selection = ordersByCustomerAndStore.Select (c => new CustomerOrderModel {

                OrderNumber = c.Id,
                Timestamp = c.Timestamp,
                StoreName = c.Store.Name,
                CustomerName = c.Customer.Firstname + " " + c.Customer.Lastname,

                OrderLine = c.OrderLine.Select (o => new OrderLineModel {

                    ProductQuantity = o.ProductQuantity,

                    Product = new ProductModel {

                        Price = o.Product.Price,
                        Name = o.Product.Name,
                        Id = o.Product.Id
                    }
                })
            });

            mLogger.LogDebug (selection.ToString ());

            return await selection.ToListAsync ();
        }
    }
}

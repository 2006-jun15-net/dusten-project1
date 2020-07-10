using Microsoft.EntityFrameworkCore;
using Project1.Business;
using Project1.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project1.DataAccess.Repository {

    public class CustomerOrderRepository : Repository, ICustomerOrderRepository {

        public CustomerOrderRepository (Project0Context context) : base (context) { }

        /// <summary>
        /// FOR UNIT TESTS ONLY!!!!
        /// </summary>
        public CustomerOrderRepository () { }

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
                        Name = o.Product.Name,
                        Id = o.Product.Id
                    }
                })
            });
        }

        public virtual IEnumerable<CustomerOrderModel> FindOrdersByCustomerAndStore (int? customerId, int storeId) {

            if (customerId == null) {
                return new List<CustomerOrderModel> ();
            }

            var ordersByCustomer = mContext.CustomerOrder.Where (c => c.CustomerId == customerId && c.StoreId == storeId)
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
                        Name = o.Product.Name,
                        Id = o.Product.Id
                    }
                })
            });
        }

        public virtual bool Add (CustomerOrderModel order, int? customerId, int storeId) {

            if (customerId == null) {
                return false;
            }

            mContext.CustomerOrder.Add (new CustomerOrder {

                CustomerId = (int)customerId,
                StoreId = storeId,

                Timestamp = order.Timestamp,

                OrderLine = order.OrderLine.Select (o => new OrderLine {

                    ProductId = o.Product.Id,
                    ProductQuantity = o.ProductQuantity

                }).ToList ()
            });

            mContext.SaveChanges ();

            return true;
        }
    }
}

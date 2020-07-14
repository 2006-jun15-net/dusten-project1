using System;
using System.Collections.Generic;

namespace Project1.Business {

    /// <summary>
    /// Business model for CustomerOrder entity
    /// </summary>
    public class CustomerOrderModel {

        /// <summary>
        /// Arbitrary maximum for products allowed in an order
        /// </summary>
        public const int MAX_PRODUCTS = 20;

        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public int OrderNumber { get; set; }
        public DateTime Timestamp { get; set; }
        public string StoreName { get; set; }
        public string CustomerName { get; set; }
        public IEnumerable<OrderLineModel> OrderLine { get; set; }

        /// <summary>
        /// Ttotal price for the entire order (product counts x unit prices)
        /// </summary>
        public string TotalPrice {

            get {

                double total = 0.0;

                foreach (var line in OrderLine) {
                    total += line.Product.Price * line.ProductQuantity;
                }

                return $"${total:0.00}";
            }
        }

        /// <summary>
        /// Total count of all products in the order
        /// </summary>
        public int ProductCount {

            get {

                int total = 0;

                foreach (var line in OrderLine) {
                    total += line.ProductQuantity;
                }

                return total;
            }
        }
    }
}

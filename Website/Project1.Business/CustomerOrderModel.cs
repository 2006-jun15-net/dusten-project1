using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Business {
    public class CustomerOrderModel {

        public const int MAX_PRODUCTS = 20;

        public int OrderNumber { get; set; }
        public DateTime Timestamp { get; set; }
        public string StoreName { get; set; }
        public string CustomerName { get; set; }
        public IEnumerable<OrderLineModel> OrderLine { get; set; }

        public string TotalPrice {

            get {

                double total = 0.0;

                foreach (var line in OrderLine) {
                    total += line.Product.Price * line.ProductQuantity;
                }

                return $"${total:0.00}";
            }
        }
    }
}

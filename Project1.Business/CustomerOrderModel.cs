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

        public override string ToString () {

            // TODO HTML code instead of raw string?

            string output = $"Order #{OrderNumber} placed at {StoreName} on {Timestamp:MM/dd/yy}:\n";

            double total = 0.0;

            foreach (var line in OrderLine) {

                output += $"\t{line}";
                total += line.Product.Price * line.ProductQuantity;
            }

            output += $"\n\tOrder total: ${total:#.00}\n";
            return output;
        }
    }
}

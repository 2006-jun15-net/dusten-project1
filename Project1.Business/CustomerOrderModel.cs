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

        /// <summary>
        /// Print all info for the order
        /// </summary>
        public void ShowInfo () {

            Console.WriteLine ($"Order #{OrderNumber} placed at {StoreName} on {Timestamp:MM/dd/yy}:\n");

            double total = 0.0;

            foreach (var line in OrderLine) {

                Console.WriteLine ($"\t{line}");
                total += line.Product.Price * line.ProductQuantity;
            }

            Console.Write($"\n\tOrder total: ${total:#.00}\n");
        }
    }
}

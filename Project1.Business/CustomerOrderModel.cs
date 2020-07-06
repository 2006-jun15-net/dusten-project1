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

        public void ShowInfoForStore () {

            Console.WriteLine ($"Order #{OrderNumber} placed at {StoreName} on {Timestamp:MM/dd/yy}:\n");

            ShowInfo ();
            Console.WriteLine ();
        }

        /// <summary>
        /// Print all info of the order from it's specified customer
        /// </summary>
        public void ShowInfoForCustomer () {

            Console.WriteLine ($"Order #{OrderNumber} placed by {CustomerName} on {Timestamp:MM/dd/yy}:\n");

            ShowInfo ();
            Console.WriteLine ();
        }

        /// <summary>
        /// Print all info for the order
        /// </summary>
        public void ShowInfo () {

            double total = 0.0;

            foreach (var line in OrderLine) {

                Console.WriteLine ($"\t{line}");
                total += line.Product.Price * line.ProductQuantity;
            }

            Console.Write($"\n\tOrder total: ${total:#.00}");
        }
    }
}

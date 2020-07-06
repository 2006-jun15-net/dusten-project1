using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Business {

    public class StoreModel {

        public string Name { get; set; }
        public IEnumerable<StoreStockModel> StoreStock { get; set; }

        public void ShowProductStock () {

            Console.WriteLine ($"\nProducts for {Name}:\n");

            foreach (var stock in StoreStock) {
                Console.WriteLine ($"\t{stock}");
            }

            Console.WriteLine ();
        }
    }
}

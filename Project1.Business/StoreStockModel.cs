using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Business {

    public class StoreStockModel {

        public ProductModel Product { get; set; }
        public int ProductQuantity { get; set; }

        public override string ToString () {
            return $"{Product.Name} ({ProductQuantity})";
        }
    }
}

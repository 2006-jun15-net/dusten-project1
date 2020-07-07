﻿namespace Project1.Business {

    public class OrderLineModel {

        public int ProductQuantity { get; set; }
        public ProductModel Product { get; set; }

        public override string ToString () {

            double totalPrice = Product.Price * ProductQuantity;
            return $"{Product.Name} (${Product.Price:0.00}) x {ProductQuantity}: ${totalPrice:0.00}";
        }
    }
}

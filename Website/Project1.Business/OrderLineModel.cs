using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace Project1.Business {

    public class OrderLineModel {

        [Min(0)]
        [Display(Name = "Quantity")]
        public int ProductQuantity { get; set; }
        public ProductModel Product { get; set; }

        public override string ToString () {

            double totalPrice = Product.Price * ProductQuantity;
            return $"{Product.Name} (${Product.Price:0.00}) x {ProductQuantity}: ${totalPrice:0.00}";
        }
    }
}

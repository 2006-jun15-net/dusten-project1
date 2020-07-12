using System.ComponentModel.DataAnnotations;
using Project1.Business.Validation;

namespace Project1.Business {

    /// <summary>
    /// Business model for OrderLine entity
    /// </summary>
    public class OrderLineModel {

        [MinValue (0)]
        [Display (Name = "Quantity")]
        public int ProductQuantity { get; set; }
        public ProductModel Product { get; set; }

        /// <summary>
        /// Show details for the corresponding order
        /// </summary>
        /// <returns></returns>
        public override string ToString () {

            double totalPrice = Product.Price * ProductQuantity;
            return $"{Product.Name} (${Product.Price:0.00}) x {ProductQuantity}: ${totalPrice:0.00}";
        }
    }
}

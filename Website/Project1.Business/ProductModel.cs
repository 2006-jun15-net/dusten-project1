using System.ComponentModel.DataAnnotations;

namespace Project1.Business {

    public class ProductModel {

        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Display(Name = "Product Price")]
        public double Price { get; set; }

        public string DisplayPrice {
            get => $"${Price:0.00}";
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Project1.Business {

    /// <summary>
    /// Business model for Product entity
    /// </summary>
    public class ProductModel {

        public int Id { get; set; }

        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Display(Name = "Product Price")]
        public double Price { get; set; }

        /// <summary>
        /// Use currency formatting to properly display
        /// price in views
        /// </summary>
        public string DisplayPrice {
            get => $"${Price:0.00}";
        }
    }
}

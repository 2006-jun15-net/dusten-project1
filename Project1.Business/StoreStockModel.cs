using DataAnnotationsExtensions;

namespace Project1.Business {

    public class StoreStockModel {

        [Min(0)]
        public ProductModel Product { get; set; }
        public int ProductQuantity { get; set; }
    }
}

using Project1.Business.Validation;

namespace Project1.Business {

    public class StoreStockModel {

        [MinValue (0)]
        public ProductModel Product { get; set; }
        public int ProductQuantity { get; set; }
    }
}

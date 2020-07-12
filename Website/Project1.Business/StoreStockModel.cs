using Project1.Business.Validation;

namespace Project1.Business {

    /// <summary>
    /// Business model for StoreStock entity
    /// </summary>
    public class StoreStockModel {

        public ProductModel Product { get; set; }

        [MinValue (0)]
        public int ProductQuantity { get; set; }
    }
}

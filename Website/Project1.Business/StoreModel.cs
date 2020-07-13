using System.Collections.Generic;

namespace Project1.Business {

    /// <summary>
    /// Business model for Store entity
    /// </summary>
    public class StoreModel {

        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<StoreStockModel> StoreStock { get; set; }
    }
}

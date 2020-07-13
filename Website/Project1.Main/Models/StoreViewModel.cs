using Project1.Business;
using System.Collections.Generic;

namespace Project1.Main.Models {
    public class StoreViewModel {

        public string Name { get; set; }
        public bool HasCustomer { get; set; }
        public IEnumerable<StoreStockModel> Stock { get; set; }
    }
}

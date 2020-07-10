using Project1.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Main.Models {
    public class StoreViewModel {

        public string Name { get; set; }
        public bool HasCustomer { get; set; }
        public IEnumerable<StoreStockModel> Stock { get; set; }
    }
}

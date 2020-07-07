using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Business {

    public class StoreModel {

        public string Name { get; set; }
        public IEnumerable<StoreStockModel> StoreStock { get; set; }
    }
}

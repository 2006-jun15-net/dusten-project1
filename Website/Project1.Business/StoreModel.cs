using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Business {

    public class StoreModel {

        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<StoreStockModel> StoreStock { get; set; }
    }
}

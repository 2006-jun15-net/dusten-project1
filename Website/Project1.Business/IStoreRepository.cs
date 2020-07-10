using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Business {

    public interface IStoreRepository {

        public IEnumerable<StoreModel> FindAll { get; }

        StoreModel FindByName (string name);
    }
}

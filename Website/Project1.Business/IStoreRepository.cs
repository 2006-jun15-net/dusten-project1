using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Business {

    /// <summary>
    /// Store repository interface
    /// </summary>
    public interface IStoreRepository {

        public IEnumerable<StoreModel> FindAll { get; }

        Task<StoreModel> FindByName (string name);
    }
}

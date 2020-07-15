using System.Threading.Tasks;

namespace Project1.Business {

    public interface IStoreStockRepository {

        Task<bool> RemoveQuantityAsync (string productName, int storeId, int quantity);
    }
}

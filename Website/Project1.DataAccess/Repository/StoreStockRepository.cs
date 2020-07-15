using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project1.Business;
using Project1.DataAccess.Model;

namespace Project1.DataAccess.Repository {

    public class StoreStockRepository : Repository, IStoreStockRepository {

        private readonly ILogger<StoreStockRepository> mLogger;

        public StoreStockRepository (ILogger<StoreStockRepository> logger,
                                    Project0Context context) : base (context) {
            mLogger = logger;
        }
        
        /// <summary>
        /// FOR UNIT TESTS ONLY!!!!
        /// </summary>
        public StoreStockRepository () { }

        public virtual async Task<bool> RemoveQuantityAsync (string productName, int storeId, int quantity) {

            var found = mContext.StoreStock.Where (s => s.StoreId == storeId)
                                .Include (s => s.Product).Where (s => s.Product.Name == productName);

            if (mLogger != null) {
                mLogger.LogInformation (found.ToString ());
            }

            var removed = await found.FirstOrDefaultAsync ();

            if (removed == default || removed.ProductQuantity < quantity) {
                return false;
            }

            if (mLogger != null) {
                mLogger.LogInformation (mContext.StoreStock.Where (s => s.Id == removed.Id).ToString ());
            }

            mContext.StoreStock.Where (s => s.Id == removed.Id).First ().ProductQuantity -= quantity;
            await mContext.SaveChangesAsync ();

            return true;
        }
    }
}

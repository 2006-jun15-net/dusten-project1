using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Project1.DataAccess.Model;
using Project1.Business;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Project1.DataAccess.Repository {

    /// <summary>
    /// Repository for 'Store' table
    /// </summary>
    public class StoreRepository : Repository, IStoreRepository {

        private readonly ILogger<StoreRepository> mLogger;

        public StoreRepository (ILogger<StoreRepository> logger, 
                                Project0Context context) : base (context) { 
            mLogger = logger;
        }

        /// <summary>
        /// FOR UNIT TESTS ONLY!!!!
        /// </summary>
        public StoreRepository () { }

                /// <summary>
        /// Find all Store entities and map to models
        /// </summary>
        public virtual async Task<IEnumerable<StoreModel>> FindAllAsync() {
                
            IQueryable<StoreModel> selection = mContext.Store.Select (s => new StoreModel { Name = s.Name });
            mLogger.LogInformation (selection.ToString ());

            return await selection.ToListAsync ();
        }

        /// <summary>
        /// Find Store entity with given name and map to model
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual async Task<StoreModel> FindByNameAsync (string name) {

            IQueryable<StoreModel> selection = mContext.Store.Where (s => s.Name == name)
                .Include (s => s.StoreStock).ThenInclude (st => st.Product)
                .Select (s => new StoreModel {

                    StoreStock = s.StoreStock.Select (st => new StoreStockModel {

                        Product = new ProductModel {

                            Name = st.Product.Name,
                            Price = st.Product.Price,
                            Id = st.Product.Id
                        },
                        ProductQuantity = st.ProductQuantity
                    }),
                    Name = s.Name,
                    Id = s.Id

                });

            mLogger.LogInformation (selection.ToString ());

            return await selection.FirstOrDefaultAsync ();
        }
    }
}
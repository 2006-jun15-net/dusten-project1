using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Project1.DataAccess.Model;
using Project1.Business;

namespace Project1.DataAccess.Repository {

    public class StoreRepository : Repository, IStoreRepository {

        public IEnumerable<StoreModel> FindAll {
            get => mContext.Store.Select (s => new StoreModel { Name = s.Name });
        }

        public StoreRepository (Project0Context context)
            : base (context) { }

        /// <summary>
        /// FOR UNIT TESTS ONLY!!!!
        /// </summary>
        public StoreRepository () { }

        public virtual StoreModel FindByName (string name) {

            System.Diagnostics.Debug.WriteLine ($"Store name: {name}");

            return mContext.Store.Where (s => s.Name == name)
                .Include (s => s.StoreStock).ThenInclude (st => st.Product)
                .Select (s => new StoreModel {

                    StoreStock = s.StoreStock.Select (st => new StoreStockModel {

                        Product = new ProductModel {

                            Name = st.Product.Name,
                            Price = st.Product.Price
                        },
                        ProductQuantity = st.ProductQuantity
                    }),
                    Name = s.Name
                }).First ();
        }
    }
}
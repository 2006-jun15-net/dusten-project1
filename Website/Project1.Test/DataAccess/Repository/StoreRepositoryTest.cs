using Microsoft.EntityFrameworkCore;

using Project1.Business;
using Project1.DataAccess.Model;
using Project1.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Project1.Test.DataAccess.Repository {

    public class StoreRepositoryTest {

        private readonly Project0Context mContext;

        public StoreRepositoryTest () {

            mContext = new Project0Context (new DbContextOptionsBuilder<Project0Context> ()
                .UseInMemoryDatabase (databaseName: "StoreDatabase")
                .Options);

            mContext.Database.EnsureDeleted ();

            mContext.Store.Add (new Store {

                Name = "Test One",

                StoreStock = new List<StoreStock> {

                    new StoreStock {

                        Product = new Product {
                            Name = "Test Product"
                        }
                    }
                }
            });
            mContext.SaveChanges ();
        }

        [Fact]
        public void TestFindAll () {

            var storeRepository = new StoreRepository (null, mContext);
            var allStores = storeRepository.FindAllAsync ().Result;

            Assert.Single (allStores);
        }

        [Fact]
        public void TestFindByNameSuccess () {

            var storeRepository = new StoreRepository (null, mContext);
            var storeByName = storeRepository.FindByNameAsync ("Test One").Result;

            Assert.NotSame (default (StoreModel), storeByName);
            Assert.Equal ("Test One", storeByName.Name);
        }

        [Fact]
        public void TestFindByNameFail () {

            var storeRepository = new StoreRepository (null, mContext);
            var storeByName = storeRepository.FindByNameAsync ("Not a store").Result;

            Assert.Same (default (StoreModel), storeByName);
        }
    }
}

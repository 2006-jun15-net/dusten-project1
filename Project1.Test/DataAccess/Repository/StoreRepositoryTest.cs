using System.Linq;
using System.Collections.Generic;

using Xunit;
using Moq;

using Project1.DataAccess.Repository;
using Project1.DataAccess.Model;
using Project1.Business;

namespace Project1.Test.DataAccess.Repository {

    public class StoreRepositoryTest {

        private readonly StoreRepository mStoreRepository;

        public StoreRepositoryTest () {

            var mockStoreRepo = new Mock<StoreRepository> ();

            List<StoreModel> stores = new List<StoreModel> () {

                new StoreModel {
                    Name = "Test"
                }
            };

            // Find by name
            mockStoreRepo.Setup (
                repo => repo.FindByName (It.IsAny<string> ())
            ).Returns (
                (string name) => stores.SingleOrDefault (s => s.Name == name)
            );

            mockStoreRepo.SetupAllProperties ();

            mStoreRepository = mockStoreRepo.Object;
        }

        [Fact]
        public void TestFindByNameSuccess () {

            var storeByName = mStoreRepository.FindByName ("Test");
            Assert.NotSame (default(Store), storeByName);
        }

        [Fact]
        public void TestFindByNameFail () {

            var storeByName = mStoreRepository.FindByName ("Not a store");
            Assert.Same (default(Store), storeByName);
        }
    }
}

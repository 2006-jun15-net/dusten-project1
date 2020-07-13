using Moq;
using Project1.Business;
using Project1.DataAccess.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

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

            // Find all
            mockStoreRepo.Setup (
                repo => repo.FindAllAsync ()
            ).Returns (
                async () => await Task.Run (() => stores)
            );

            // Find by name
            mockStoreRepo.Setup (
                repo => repo.FindByNameAsync (It.IsAny<string> ())
            ).Returns (
                async (string name) =>
                await Task.Run (() => stores.Where (s => s.Name == name).FirstOrDefault ())
            );

            mockStoreRepo.SetupAllProperties ();

            mStoreRepository = mockStoreRepo.Object;
        }

        [Fact]
        public async void TestFindAll () {

            var allStores = await mStoreRepository.FindAllAsync ();

            Assert.Single (allStores);
        }

        [Fact]
        public async void TestFindByNameSuccess () {

            var storeByName = await mStoreRepository.FindByNameAsync ("Test");

            Assert.NotSame (default (StoreModel), storeByName);
            Assert.Equal ("Test", storeByName.Name);
        }

        [Fact]
        public async void TestFindByNameFail () {

            var storeByName = await mStoreRepository.FindByNameAsync ("Not a store");

            Assert.Same (default (StoreModel), storeByName);
        }
    }
}

using Xunit;

using Project1.Business;
using Project1.DataAccess.Model;

namespace Project1.Test.Business {

    public class OrderBuilderTest {

        private readonly OrderBuilder mOrderBuilder;
        private readonly Store mTestStore;

        public OrderBuilderTest () {

            var testProduct = new Product {
                Name = "Test"
            };

            var testStock = new StoreStock {
                Product = testProduct,
                ProductQuantity = 100
            };

            mTestStore = new Store {
                StoreStock = { testStock }
            };

            mOrderBuilder = new OrderBuilder ();
        }

        [Fact]
        public void TestAddProductSuccess () {

            // No thrown exception = success
            mOrderBuilder.AddProduct (mTestStore, "Test", 2);
        }

        [Fact]
        public void TestAddProductFailQuantity () {

            // Quantity should not be allowed
            Assert.Throws<BusinessLogicException> (
                () => mOrderBuilder.AddProduct (mTestStore, "Test", 21)
            );
        }

        [Fact]
        public void TestAddProductFailName () {

            // Name should not exist
            Assert.Throws<BusinessLogicException> (
                () => mOrderBuilder.AddProduct (mTestStore, "Bacon", 2)
            );
        }
    }
}

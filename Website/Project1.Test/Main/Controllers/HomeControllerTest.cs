using Xunit;

using Project1.Business;
using System.Threading.Tasks;
using Project1.Main.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Project1.Test.Main.Controllers {

    public class HomeControllerTest {

        private readonly HomeController mHomeController;

        public HomeControllerTest () {
            mHomeController = new HomeController (null);
        }

        [Fact]
        public void KeepSessionAlive () {

            JsonResult request = mHomeController.KeepSessionAlive () as JsonResult;

            // JSON is created with generic objects, we need
            // to make it more concrete to test properly
            var requestObject = (Result)request.Value;

            Assert.NotNull (request);
            Assert.True(requestObject.success);
        }

        private struct Result {
            public bool success;
        }
    }
}

﻿using Microsoft.AspNetCore.Mvc;
using Project1.Main;
using Project1.Main.Controllers;
using Xunit;

namespace Project1.Test.Main.Controllers {

    public class HomeControllerTest {

        private readonly HomeController mHomeController;

        public HomeControllerTest () {
            mHomeController = new HomeController (null);
        }

        [Fact]
        public void KeepSessionAlive () {

            JsonResult request = mHomeController.KeepSessionAlive () as JsonResult;
            JsonResponse jsonValue = (JsonResponse)request.Value;

            Assert.NotNull (request);
            Assert.Equal (JsonResponse.Success, jsonValue);
        }
    }
}

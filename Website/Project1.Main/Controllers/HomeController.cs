using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Project1.Main.Models;

namespace Project1.Main.Controllers {

    /// <summary>
    /// Provides error and session timer requests
    /// </summary>
    public class HomeController : Controller {

        private readonly ILogger<HomeController> mLogger;

        public HomeController (ILogger<HomeController> logger) {
            mLogger = logger;    
        }

        /// <summary>
        /// Timed request that keeps session values alive by 
        /// implicitly resending the cookie data to refresh the 
        /// session's accessor timeout
        /// </summary>
        /// <returns>JSON object with 'success = true'</returns>
        [HttpPost]
        public IActionResult KeepSessionAlive () {

            if (mLogger != null) {
                mLogger.LogInformation ("HomeController instance created");
            }

            return Json (new { success = true });
        }

        /// <summary>
        /// Returns a page with a view of the current HTTP error 
        /// </summary>
        /// <returns>View model with the current HTTP error</returns>
        [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error () {

            if (mLogger != null) {
                mLogger.LogError ("Home/Error request");
            }

            return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

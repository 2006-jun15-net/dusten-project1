using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Project1.Main.Controllers {

    public class LoggedController : Controller {

        protected ILogger mLogger;

        public LoggedController(ILogger logger) {
            mLogger = logger;
        }
    }
}

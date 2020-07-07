using Microsoft.AspNetCore.Mvc;

using Project1.Business;
using Project1.Main.Models;

namespace Project1.Main.Controllers {

    public class StoreController : Controller {

        private readonly IStoreRepository mStoreRepository;

        public StoreController (IStoreRepository storeRepository) {
            mStoreRepository = storeRepository;
        }

        public IActionResult Index (string name) {

            if (!ModelState.IsValid) {
                return BadRequest ();
            }

            var store = mStoreRepository.FindByName (name);

            if (store == default) {
                return NotFound ();
            }

            return View (new StoreViewModel {
                Name = store.Name
            });
        }
    }
}

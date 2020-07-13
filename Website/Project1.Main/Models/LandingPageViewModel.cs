using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project1.Main.Models {
    public class LandingPageViewModel {

        public string CustomerName { get; set; }

        [Display (Name = "Last Visited Store")]
        public string LastVisitedStore { get; set; }

        [Display (Name = "Options")]
        public IEnumerable<string> StoreOptions { get; set; }
    }
}

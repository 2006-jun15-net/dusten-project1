using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Main.Models {
    public class CustomerViewModel {

        [Display (Name = "Last Visited Store")]
        public string LastVisitedStore { get; set; }

        [Display (Name = "Options")]
        public IEnumerable<string> StoreOptions { get; set; }
    }
}

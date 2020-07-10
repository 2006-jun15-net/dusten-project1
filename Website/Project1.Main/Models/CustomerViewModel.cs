using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Project1.Business;

namespace Project1.Main.Models {

    public class CustomerViewModel {

        [RegularExpression (@"^[A-Z][a-z]+")]
        public string Firstname { get; set; }

        [RegularExpression (@"^[A-Z][a-z]+")]
        public string Lastname { get; set; }

        public IEnumerable<CustomerModel> CustomerOptions { get; set; }
    }
}

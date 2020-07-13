using Project1.Business;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project1.Main.Models {

    public class CustomerViewModel {

        [RegularExpression (@"^[A-Z][a-z]+")]
        public string Firstname { get; set; }

        [RegularExpression (@"^[A-Z][a-z]+")]
        public string Lastname { get; set; }

        public IEnumerable<CustomerModel> CustomerOptions { get; set; }
    }
}

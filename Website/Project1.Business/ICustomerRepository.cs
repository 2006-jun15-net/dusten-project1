using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Business {

    /// <summary>
    /// Customer repository interface
    /// </summary>
    public interface ICustomerRepository {

        IEnumerable<CustomerModel> FindAll { get; }
        Task<CustomerModel> FindByName (string firstname, string lastname);
        bool Add (CustomerModel customerModel);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Business {

    /// <summary>
    /// Customer repository interface
    /// </summary>
    public interface ICustomerRepository {

        Task<bool> AddAsync (CustomerModel customerModel);
        Task<IEnumerable<CustomerModel>> FindAllAsync ();
        Task<CustomerModel> FindByNameAsync (string firstname, string lastname);
    }
}

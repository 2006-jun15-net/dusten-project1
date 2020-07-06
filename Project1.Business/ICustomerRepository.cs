﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Business {

    public interface ICustomerRepository {

        IEnumerable<CustomerModel> FindAll { get; }
        CustomerModel FindByName (string firstname, string lastname);
        void Add (CustomerModel customer);
    }
}

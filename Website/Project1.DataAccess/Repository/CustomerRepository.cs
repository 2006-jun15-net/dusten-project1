using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Project1.DataAccess.Model;

using Project1.Business;
using System.Diagnostics;

namespace Project1.DataAccess.Repository {

    public class CustomerRepository : Repository, ICustomerRepository {

        public IEnumerable<CustomerModel> FindAll {
            get => mContext.Customer.Select (c => new CustomerModel { Name = c.Firstname + " " + c.Lastname });
        }

        public CustomerRepository (Project0Context context) 
            : base (context) { }

        /// <summary>
        /// FOR UNIT TESTS ONLY!!!!
        /// </summary>
        public CustomerRepository () { }

        public void Add (CustomerModel customer) {

            string[] names = customer.Name.Split (" ");

            mContext.Add (new Customer {

                Firstname = names[0],
                Lastname = names[1]
            });

            mContext.SaveChanges ();
        }

        public virtual CustomerModel FindByName (string firstname, string lastname) {

            return mContext.Customer.Where (c => (c.Firstname == firstname) && (c.Lastname == lastname))
                .Include (c => c.Store).Select (c => new CustomerModel {

                    Id = c.Id,
                    Name = c.Firstname + " " + c.Lastname,

                    LastVisited = new StoreModel {
                        Name = (c.Store == default ? "" : c.Store.Name)
                    }

                }).FirstOrDefault ();
        }

        public virtual CustomerModel Add (string firstname, string lastname) {

            var existingCustomer = mContext.Customer.Where (c => c.Firstname == firstname && c.Lastname == lastname).FirstOrDefault ();

            if (existingCustomer != default) {
                return default;
            }

            mContext.Customer.Add (new Customer {
                Firstname = firstname,
                Lastname = lastname
            });

            return new CustomerModel {

                Id = null,
                Name = firstname + " " + lastname
            };
        }
    }
}

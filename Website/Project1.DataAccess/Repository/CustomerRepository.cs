using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Project1.DataAccess.Model;

using Project1.Business;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Project1.DataAccess.Repository {

    public class CustomerRepository : Repository, ICustomerRepository {

        public CustomerRepository (ILogger logger, Project0Context context) 
            : base (logger, context) { }

        /// <summary>
        /// FOR UNIT TESTS ONLY!!!!
        /// </summary>
        public CustomerRepository () { }

        /// <summary>
        /// Create new Customer entity in db from given model
        /// </summary>
        /// <param name="customer"></param>
        public virtual async Task<bool> AddAsync (CustomerModel customer) {

            string[] names = customer.Name.Split (" ");

            var firstname = names[0];
            var lastname = names[1];

            var existingCustomer = mContext.Customer.Where (c => c.Firstname == firstname && c.Lastname == lastname).FirstOrDefault ();

            if (existingCustomer != default) {
                return false;
            }

            var added = mContext.Add (new Customer {

                Firstname = names[0],
                Lastname = names[1]
            });

            mLogger.LogDebug (added.ToString ());

            await mContext.SaveChangesAsync ();

            return true;
        }

                /// <summary>
        /// Find all Customer entites and map to moodels
        /// </summary>
        public virtual async Task<IEnumerable<CustomerModel>> FindAllAsync () {

            IQueryable<CustomerModel> selection = mContext.Customer.Select (c => new CustomerModel { Name = c.Firstname + " " + c.Lastname });
            mLogger.LogDebug (selection.ToString ());

            return await selection.ToListAsync ();
        }

        public virtual async Task<CustomerModel> FindByNameAsync (string firstname, string lastname) {

            IQueryable<CustomerModel> selection = mContext.Customer.Where (c => (c.Firstname == firstname) && (c.Lastname == lastname))
                .Include (c => c.Store).Select (c => new CustomerModel {

                    Id = c.Id,
                    Name = c.Firstname + " " + c.Lastname,

                    LastVisited = new StoreModel {
                        Name = (c.Store == default ? "" : c.Store.Name)
                    }

                });

            mLogger.LogDebug (selection.ToString ());

            return await selection.FirstOrDefaultAsync ();
        }
    }
}

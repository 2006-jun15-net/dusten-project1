using Project1.DataAccess.Model;

namespace Project1.DataAccess.Repository {

    /// <summary>
    /// Base class for all repositories
    /// </summary>
    public class Repository {

        protected readonly Project0Context mContext;

        protected Repository (Project0Context context) {
            mContext = context;
        }

        /// <summary>
        /// FOR UNIT TESTS ONLY!!!!
        /// </summary>
        protected Repository () { }
    }
}

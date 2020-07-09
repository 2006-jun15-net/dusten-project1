using Project1.DataAccess.Model;

namespace Project1.DataAccess.Repository {

    public class Repository {

        protected readonly Project0Context mContext;

        protected Repository (Project0Context context) {
            mContext = context;
        }

        protected Repository () { }
    }
}

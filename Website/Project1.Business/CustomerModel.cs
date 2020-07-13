namespace Project1.Business {

    /// <summary>
    /// Business model for Customer entity
    /// </summary>
    public class CustomerModel {

        private StoreModel mLastVisited;

        public int? Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Store that this customer visited last time they
        /// were logged in
        /// </summary>
        public StoreModel LastVisited {

            get {

                if (mLastVisited == default) {

                    return new StoreModel {
                        Name = ""
                    };
                }

                return mLastVisited;
            }
            set => mLastVisited = value;
        }
    }
}

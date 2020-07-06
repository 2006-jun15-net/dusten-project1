using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Business {
    public class CustomerModel {

        private StoreModel mLastVisited;

        public string Name { get; set; }
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

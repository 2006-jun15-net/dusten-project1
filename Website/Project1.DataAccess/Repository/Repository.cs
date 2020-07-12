﻿using Microsoft.Extensions.Logging;
using Project1.DataAccess.Model;

namespace Project1.DataAccess.Repository {

    /// <summary>
    /// Base class for all repositories
    /// </summary>
    public class Repository {

        protected readonly Project0Context mContext;
        protected readonly ILogger mLogger;

        protected Repository (ILogger logger, Project0Context context) {

            mLogger  = logger;
            mContext = context;
        }

        /// <summary>
        /// FOR UNIT TESTS ONLY!!!!
        /// </summary>
        protected Repository () { }
    }
}

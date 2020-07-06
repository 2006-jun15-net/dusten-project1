﻿using Xunit;

using Project1.Business;

namespace Project1.Test.Business {

    public class CustomerBuilderTest {

        readonly CustomerBuilder mCustomerBuilder;

        public CustomerBuilderTest () {
            mCustomerBuilder = new CustomerBuilder ();
        }

        [Fact]
        public void TestBuildSuccess () {

            var customer = mCustomerBuilder.Build ("Test Customer");

            Assert.Equal ("Test", customer.Firstname);
            Assert.Equal ("Customer", customer.Lastname);
        }

        [Fact]
        public void TestBuildFailure () {

            Assert.Throws<BusinessLogicException> (
                () => mCustomerBuilder.Build ("AgentSmith")
            );
        }
    }
}

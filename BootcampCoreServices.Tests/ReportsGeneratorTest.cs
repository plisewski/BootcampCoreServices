using BootcampCoreServices.Model;
using BootcampCoreServices.ViewModel;
using System.Collections.Generic;
using Xunit;

namespace BootcampCoreServices.Tests
{
    public class ReportsGeneratorTest
    {
        private readonly List<Request> _requestList = new List<Request>
        {
            new Request
            {
                ClientId = "1",
                RequestId = 1,
                Name = "Chleb",
                Quantity = 2,
                Price = 50.00
            },
            new Request
            {
                ClientId = "1",
                RequestId = 1,
                Name = "Chleb",
                Quantity = 2,
                Price = 50.00
            },
            new Request
            {
                ClientId = "2",
                RequestId = 1,
                Name = "Chleb",
                Quantity = 2,
                Price = 50.00
            }
        };

        [Fact]
        public void TotalNumberOfRequestsTest()
        {
            int expectedNumber = 2;
            int actualNumber = ReportsGenerator.TotalNumberOfRequests(_requestList);
            Assert.Equal(expectedNumber, actualNumber);
        }

        [Fact]
        public void TotalNumberOfRequestsClientId()
        {
            int expectedNumber = 1;
            int actualNumber = ReportsGenerator.TotalNumberOfRequests(_requestList, "1");
            Assert.Equal(expectedNumber, actualNumber);
        }

        [Fact]
        public void TotalValueOfRequests()
        {
            double expectedValue = 300;
            double actualValue = ReportsGenerator.TotalValueOfRequests(_requestList);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void TotalValueOfRequestsClientId()
        {
            double expectedValue = 100;
            double actualValue = ReportsGenerator.TotalValueOfRequests(_requestList, "2");
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void RequestAverageValue()
        {
            double expectedValue = 150;
            double actualValue = ReportsGenerator.TotalValueOfRequests(_requestList) / ReportsGenerator.TotalNumberOfRequests(_requestList);
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void RequestAverageValueClientId()
        {
            double expectedValue = 200;
            double actualValue = ReportsGenerator.TotalValueOfRequests(_requestList, "1") / ReportsGenerator.TotalNumberOfRequests(_requestList, "1");
            Assert.Equal(expectedValue, actualValue);
        }
    }
}

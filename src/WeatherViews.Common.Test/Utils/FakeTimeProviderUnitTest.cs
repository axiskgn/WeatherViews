using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherViews.Common.Utils;

namespace WeatherViews.Common.Test.Utils
{
    [TestClass]
    public class FakeTimeProviderUnitTest
    {
        [TestMethod]
        public void FakeTimeProviderCreateTest()
        {
            var provider = new FakeTimeProvider();

            var firstTime = provider.UtcNow;
            Thread.Sleep(50);
            var secondTime = provider.UtcNow;

            Assert.AreEqual(firstTime, secondTime);
        }

        [TestMethod]
        public void FakeTimeProviderSetValueTest()
        {
            var testTime = new DateTime(2000,01,27,12,55,57);
            var provider = new FakeTimeProvider();
            Assert.AreNotEqual(testTime, provider.UtcNow);

            provider.SetTime(testTime);

            Assert.AreEqual(testTime, provider.UtcNow);
        }

        [TestMethod]
        public void FakeTimeProviderSetValueConstruectorTest()
        {
            var testTime = new DateTime(2000, 01, 27, 12, 55, 57);
            var provider = new FakeTimeProvider(testTime);
            Assert.AreEqual(testTime, provider.UtcNow);
        }
    }
}

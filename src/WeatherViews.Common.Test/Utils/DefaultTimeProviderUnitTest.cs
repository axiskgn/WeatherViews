using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherViews.Common.Utils;

namespace WeatherViews.Common.Test.Utils
{
    [TestClass]
    public class DefaultTimeProviderUnitTest
    {
        [TestMethod]
        public void DefaultTimeProviderChangingUtcNowTest()
        {
            var provider = new DefaultTimeProvider();
            var firstValue = provider.UtcNow;
            Thread.Sleep(50);
            var secondValue = provider.UtcNow;
            Assert.AreNotEqual(firstValue, secondValue);
        }
    }
}

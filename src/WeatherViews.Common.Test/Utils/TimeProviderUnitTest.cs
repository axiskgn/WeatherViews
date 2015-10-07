using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WeatherViews.Common.Utils;

namespace WeatherViews.Common.Test.Utils
{
    [TestClass]
    public class TimeProviderUnitTest
    {
        [TestMethod]
        public void TimeProviderResetToDefaultTest()
        {
            TimeProvider.ResetToDefault();
            Assert.AreEqual(typeof(DefaultTimeProvider),TimeProvider.Current.GetType());
        }

        [TestMethod]
        public void TimeProviderSetCurrentTest()
        {
            var moqProvider = new Mock<TimeProvider>();
            TimeProvider.Current = moqProvider.Object;
            Assert.AreEqual(moqProvider.Object, TimeProvider.Current);
        }
    }
}

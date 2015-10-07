using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WeatherViews.Common.DataProvider;
using WeatherViews.Common.DataSources;
using WeatherViews.Common.Entity;
using WeatherViews.Common.Utils;

namespace WeatherViews.Common.Test.DataSource
{
    [TestClass]
    public class CashedResultWeatherViewDataSourceUnitTest
    {
        [TestMethod]
        public void CashedResultWeatherViewDataSourceCreateTest()
        {
            var mockSouce = new Mock<IWeatherViewDataSource>();
            var provider = new CashedResultWeatherViewDataSource(mockSouce.Object, 1);
            Assert.IsNotNull(provider);
        }

        [TestMethod]
        public void CashedResultWeatherViewDataSourceGetWeatherDataProvidersFirstTest()
        {
            var weatherDataProviders =  new[] {new Mock<IDataProviderInfo>().Object};
            var mockSource = new Mock<IWeatherViewDataSource>();
            mockSource.Setup(t => t.GetWeatherDataProviders()).Returns(weatherDataProviders);

            var provider = new CashedResultWeatherViewDataSource(mockSource.Object, 1);
            provider.ClearCash();
            var providers = provider.GetWeatherDataProviders();

            mockSource.Verify(t=>t.GetWeatherDataProviders(),Times.Once);
            Assert.AreEqual(weatherDataProviders, providers);
        }

        [TestMethod]
        public void CashedResultWeatherViewDataSourceGetWeatherDataProvidersCashTest()
        {
            var weatherDataProviders = new[] { new Mock<IDataProviderInfo>().Object };
            var mockSource = new Mock<IWeatherViewDataSource>();
            mockSource.Setup(t => t.GetWeatherDataProviders()).Returns(weatherDataProviders);
            TimeProvider.Current= new FakeTimeProvider(DateTime.Now);

            var provider = new CashedResultWeatherViewDataSource(mockSource.Object, 100);
            var providers1 = provider.GetWeatherDataProviders();
            var providers2 = provider.GetWeatherDataProviders();

            mockSource.Verify(t => t.GetWeatherDataProviders(), Times.Once);
            Assert.AreEqual(weatherDataProviders, providers1);
            Assert.AreEqual(weatherDataProviders, providers2);
        }

        [TestMethod]
        public void CashedResultWeatherViewDataSourceGetWeatherDataProvidersNotCashTest()
        {
            var weatherDataProviders = new[] { new Mock<IDataProviderInfo>().Object };
            var mockSource = new Mock<IWeatherViewDataSource>();
            mockSource.Setup(t => t.GetWeatherDataProviders()).Returns(weatherDataProviders);
            var timeProvider = new FakeTimeProvider(DateTime.Now);
            TimeProvider.Current = timeProvider;

            var provider = new CashedResultWeatherViewDataSource(mockSource.Object, 100);
            provider.GetWeatherDataProviders();

            timeProvider.SetTime(timeProvider.UtcNow.AddSeconds(101));
            provider.GetWeatherDataProviders();

            mockSource.Verify(t => t.GetWeatherDataProviders(), Times.Exactly(2));
        }

        [TestMethod]
        public void CashedResultWeatherViewDataSourceGetWeatherInfoFirstTest()
        {
            const string testName = "testName";
            var resultWeatherInfo = new Mock<ICityWeatherInfo>();
            var mockSource = new Mock<IWeatherViewDataSource>();
            mockSource.Setup(t => t.GetWeatherInfo(It.IsAny<string>())).Returns(resultWeatherInfo.Object);
            
            var provider = new CashedResultWeatherViewDataSource(mockSource.Object, 1);
            provider.ClearCash();
            var weatherInfo = provider.GetWeatherInfo(testName);

            mockSource.Verify(t => t.GetWeatherInfo(It.Is<string>(k=>k== testName)), Times.Once);
            Assert.AreEqual(resultWeatherInfo.Object, weatherInfo);
        }

        [TestMethod]
        public void CashedResultWeatherViewDataSourceGetWeatherInfoCashTest()
        {
            const string testName = "testName";
            var resultWeatherInfo = new Mock<ICityWeatherInfo>();
            var mockSource = new Mock<IWeatherViewDataSource>();
            mockSource.Setup(t => t.GetWeatherInfo(It.IsAny<string>())).Returns(resultWeatherInfo.Object);
            
            var provider = new CashedResultWeatherViewDataSource(mockSource.Object, 1);
            provider.ClearCash();
            var weatherInfo1 = provider.GetWeatherInfo(testName);
            var weatherInfo2 = provider.GetWeatherInfo(testName);

            mockSource.Verify(t => t.GetWeatherInfo(It.Is<string>(k=>k== testName)), Times.Once);
            Assert.AreEqual(resultWeatherInfo.Object, weatherInfo1);
            Assert.AreEqual(resultWeatherInfo.Object, weatherInfo2);
        }

        [TestMethod]
        public void CashedResultWeatherViewDataSourceGetWeatherInfoNotCashTest()
        {
            const string testName = "testName";
            var resultWeatherInfo = new Mock<ICityWeatherInfo>();
            var mockSource = new Mock<IWeatherViewDataSource>();
            mockSource.Setup(t => t.GetWeatherInfo(It.IsAny<string>())).Returns(resultWeatherInfo.Object);
            var timeProvider = new FakeTimeProvider(DateTime.Now);
            TimeProvider.Current = timeProvider;

            var provider = new CashedResultWeatherViewDataSource(mockSource.Object, 100);
            provider.ClearCash();
            provider.GetWeatherInfo(testName);
            timeProvider.SetTime(timeProvider.UtcNow.AddSeconds(101));
            provider.GetWeatherInfo(testName);

            mockSource.Verify(t => t.GetWeatherInfo(It.Is<string>(k=>k== testName)), Times.Exactly(2));
        }
    }
}

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WeatherViews.Common.DataProvider;
using WeatherViews.Common.DataProvider.BaseEntity;
using WeatherViews.Common.DataSources;

namespace WeatherViews.Common.Test.DataSource
{
    [TestClass]
    public class DefaultWeatherViewDataSourceUnitTest
    {
        [TestMethod]
        public void DefaultWeatherViewDataSourceCreateTest()
        {
            var providerGetter = new Mock<IProviderGetter>();
            var dataSource = new DefaultWeatherViewDataSource(providerGetter.Object);
            Assert.IsNotNull(dataSource);
        }

        [TestMethod]
        public void DefaultWeatherViewDataSourceCreateNullVerifyTest()
        {
            var catchExec = false;
            try
            {
                var dataSource = new DefaultWeatherViewDataSource(null);
                Assert.IsNotNull(dataSource);
            }
            catch (ArgumentNullException)
            {
                catchExec = true;
            }
            Assert.IsTrue(catchExec);
        }

        [TestMethod]
        public void DefaultWeatherViewDataSourceGetWeatherDataProvidersTest()
        {
            var providerInfo1 = new Mock<IDataProviderInfo>();
            var providerInfo2 = new Mock<IDataProviderInfo>();
            var providerInfo3 = new Mock<IDataProviderInfo>();

            var provider1= new Mock<IWeatherDataProvider>();
            provider1.SetupGet(t => t.Info).Returns(providerInfo1.Object);

            var provider2= new Mock<IWeatherDataProvider>();
            provider2.SetupGet(t => t.Info).Returns(providerInfo2.Object);

            var provider3= new Mock<IWeatherDataProvider>();
            provider3.SetupGet(t => t.Info).Returns(providerInfo3.Object);

            var providerGetter = new Mock<IProviderGetter>();
            providerGetter.Setup(t => t.GetWeatherDataProviders())
                .Returns(new[] {provider1.Object, provider2.Object, provider3.Object});

            var dataSource = new DefaultWeatherViewDataSource(providerGetter.Object);
            var providerInfo = dataSource.GetWeatherDataProviders();

            providerGetter.Verify(t=>t.GetWeatherDataProviders(),Times.Once);
            Assert.AreEqual(3,providerInfo.Count());

            Assert.AreEqual(providerInfo1.Object, providerInfo[0]);
            Assert.AreEqual(providerInfo2.Object, providerInfo[1]);
            Assert.AreEqual(providerInfo3.Object, providerInfo[2]);

        }

        [TestMethod]
        public void DefaultWeatherViewDataSourceGetWeatherInfoTest()
        {
            var providerInfo1 = new Mock<IDataProviderInfo>();
            var providerInfo2 = new Mock<IDataProviderInfo>();

            var weatherData1 = new Mock<IWeatherData>();
            var weatherData2 = new Mock<IWeatherData>();

            var provider1 = new Mock<IWeatherDataProvider>();
            provider1.SetupGet(t => t.Info).Returns(providerInfo1.Object);
            provider1.Setup(t => t.GetWeatherData(It.IsAny<IToponymData>())).Returns(weatherData1.Object);

            var provider2 = new Mock<IWeatherDataProvider>();
            provider2.SetupGet(t => t.Info).Returns(providerInfo2.Object);
            provider2.Setup(t => t.GetWeatherData(It.IsAny<IToponymData>())).Returns(weatherData2.Object);

            var Toponym = new Mock<IToponymData>();
            var geoProvider = new Mock<IToponymDataProvider>();
            geoProvider.Setup(t => t.FindByName(It.IsAny<string>())).Returns(Toponym.Object);

            var providerGetter = new Mock<IProviderGetter>();
            providerGetter.Setup(t => t.GetWeatherDataProviders())
                .Returns(new[] { provider1.Object, provider2.Object });

            providerGetter.Setup(t => t.GetToponymProviders()).Returns(new[] {geoProvider.Object});

            var findedName = "testValue";

            var dataSource = new DefaultWeatherViewDataSource(providerGetter.Object);
            var weatherInfo = dataSource.GetWeatherInfo(findedName);

            providerGetter.Verify(t => t.GetWeatherDataProviders(), Times.Once);
            providerGetter.Verify(t => t.GetToponymProviders(), Times.Once);

            geoProvider.Verify(t => t.FindByName(It.Is<string>(s => s == findedName)),
                Times.Once);

            provider1.Verify(t=>t.GetWeatherData(It.Is<IToponymData>(k=>k== Toponym.Object)), Times.Once);
            provider2.Verify(t=>t.GetWeatherData(It.Is<IToponymData>(k=>k== Toponym.Object)), Times.Once);

            Assert.IsNotNull(weatherInfo);

            Assert.AreEqual(Toponym.Object, weatherInfo.Toponym);
            Assert.AreEqual(2,weatherInfo.WeatherFromProvider.Length);

            Assert.AreEqual(weatherData1.Object, weatherInfo.WeatherFromProvider[0].WeatherData);
            Assert.AreEqual(providerInfo1.Object, weatherInfo.WeatherFromProvider[0].DataProviderInfo);

            Assert.AreEqual(weatherData2.Object, weatherInfo.WeatherFromProvider[1].WeatherData);
            Assert.AreEqual(providerInfo2.Object, weatherInfo.WeatherFromProvider[1].DataProviderInfo);
        }

    }
}

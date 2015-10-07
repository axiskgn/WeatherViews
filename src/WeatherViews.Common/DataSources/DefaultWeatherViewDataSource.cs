using System;
using System.Collections.Generic;
using System.Linq;
using WeatherViews.Common.DataProvider;
using WeatherViews.Common.DataProvider.BaseEntity;
using WeatherViews.Common.Entity;

namespace WeatherViews.Common.DataSources
{

    /// <summary>
    /// Реализация источника данных по умолчанию
    /// </summary>
    public class DefaultWeatherViewDataSource: IWeatherViewDataSource
    {
        private readonly IProviderGetter _providerGetter;

        public DefaultWeatherViewDataSource(IProviderGetter providerGetter)
        {
            if (providerGetter == null) throw new ArgumentNullException(nameof(providerGetter));
            _providerGetter = providerGetter;
        }

        public IDataProviderInfo[] GetWeatherDataProviders()
        {
            var providers = _providerGetter.GetWeatherDataProviders();
            return providers.Select(t => t.Info).ToArray();
        }

        public ICityWeatherInfo GetWeatherInfo(string cityName)
        {
            var geoProvider = _providerGetter.GetToponymProviders().FirstOrDefault();
            if (geoProvider == null)
            {
                //throw new ArgumentOutOfRangeException("Нет провайдеров гео данных");
                // тут надо логировать
                return null;
            }
            var toponym = geoProvider.FindByName(cityName);
            if (toponym == null)
            {
                // тут надо логировать
                return null;
            }

            var weatherList = new List<IWeatherFromProviderInfo>();
            var weatherProviders = _providerGetter.GetWeatherDataProviders();

            foreach (var weatherProvider in weatherProviders)
            {
                var weather = weatherProvider.GetWeatherData(toponym);
                weatherList.Add(new WeatherFromProviderInfo(weatherProvider.Info,weather));
            }

            return new CityWeatherInfo(toponym, weatherList.ToArray());
        }

        public bool CheckName(string cityName)
        {
            var geoProvider = _providerGetter.GetToponymProviders().FirstOrDefault();
            if (geoProvider == null)
            {
                //throw new ArgumentOutOfRangeException("Нет провайдеров гео данных");
                // тут надо логировать
                return false;
            }
            try
            {
                var toponym = geoProvider.FindByName(cityName);
                return toponym != null;
            }
            catch (NotFoundToponymException)
            {
                // не нашли город
            }
            return false;
        }

        private class CityWeatherInfo : ICityWeatherInfo
        {
            public CityWeatherInfo(IToponymData toponym, IWeatherFromProviderInfo[] weatherFromProvider)
            {
                Toponym = toponym;
                WeatherFromProvider = weatherFromProvider;
            }

            public IToponymData Toponym { get; private set; }
            public IWeatherFromProviderInfo[] WeatherFromProvider { get; private set; }
        }

        private class WeatherFromProviderInfo : IWeatherFromProviderInfo
        {
            public WeatherFromProviderInfo(IDataProviderInfo dataProviderInfo, IWeatherData weatherData)
            {
                DataProviderInfo = dataProviderInfo;
                WeatherData = weatherData;
            }

            public IDataProviderInfo DataProviderInfo { get; }
            public IWeatherData WeatherData { get; }
        }
    }
}

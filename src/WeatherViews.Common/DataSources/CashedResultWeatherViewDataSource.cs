using System;
using System.Collections.Concurrent;
using WeatherViews.Common.DataProvider;
using WeatherViews.Common.Entity;
using WeatherViews.Common.Utils;

namespace WeatherViews.Common.DataSources
{

    /// <summary>
    /// Источник данных кэширующий результаты
    /// Реализация потокобезопасна :)
    /// </summary>
    public class CashedResultWeatherViewDataSource: IWeatherViewDataSource
    {
        private readonly IWeatherViewDataSource _mainDataSource;
        private readonly int _secondsInCash;

        private DataProviderInfoCashValue _providerInfoCash;

        private readonly object _providerInfoCashLockObject = new object();

        /// <summary>
        /// Глобальный Кэш, простой вариант, можно оптимизировать (http://sergeyteplyakov.blogspot.ru/2015/06/lazy-trick-with-concurrentdictionary.html )
        /// </summary>
        private static readonly ConcurrentDictionary<string, CityWeatherInfoCashValue> WeatherInfoCash = new ConcurrentDictionary<string, CityWeatherInfoCashValue>();
        private static readonly ConcurrentDictionary<string, bool> CheckNameCash = new ConcurrentDictionary<string, bool>();

        /// <summary>
        /// Конструктор :)
        /// </summary>
        /// <param name="mainDataSource">Первоначальный источник данных</param>
        /// <param name="secondsInCash">
        /// Время жизни кэша в секндах
        /// если -1 то бесконечно
        /// </param>
        public CashedResultWeatherViewDataSource(IWeatherViewDataSource mainDataSource, int secondsInCash)
        {
            if (mainDataSource == null) throw new ArgumentNullException(nameof(mainDataSource));
            _mainDataSource = mainDataSource;
            _secondsInCash = secondsInCash;
        }

        public IDataProviderInfo[] GetWeatherDataProviders()
        {
            lock (_providerInfoCashLockObject)
            {
                if (_providerInfoCash == null ||
                    _providerInfoCash.TimeStamp.AddSeconds(_secondsInCash) <= TimeProvider.Current.UtcNow)
                {
                    _providerInfoCash = GetWeatherProvider();
                }
                return _providerInfoCash.InfoListValue;
            }
        }

        private DataProviderInfoCashValue GetWeatherProvider()
        {
            return new DataProviderInfoCashValue(_mainDataSource.GetWeatherDataProviders());
        }

        public ICityWeatherInfo GetWeatherInfo(string cityName)
        {

            CityWeatherInfoCashValue valueInCash;
            try
            {
                valueInCash = WeatherInfoCash.GetOrAdd(cityName,
                    s => new CityWeatherInfoCashValue(_mainDataSource.GetWeatherInfo(cityName)));
            }
            catch (NotFoundToponymException)
            {
                // произошла ошибка поиска, ничего не найдено
                // обработки ошибок нет
                return null;
            }

            if (valueInCash == null)
            {
                return null;
            }

            if (valueInCash.TimeStamp.AddSeconds(_secondsInCash) <= TimeProvider.Current.UtcNow)
            {
                valueInCash = GetWeatherInfoCash(cityName);
                WeatherInfoCash[cityName] = valueInCash;
            }
            return WeatherInfoCash[cityName].Weather;
        }

        public bool CheckName(string cityName)
        {
            return CheckNameCash.GetOrAdd(cityName, _mainDataSource.CheckName(cityName));
        }

        public void ClearCash()
        {
            WeatherInfoCash.Clear();
            lock (_providerInfoCashLockObject)
            {
                _providerInfoCash = null;
            }

        }

        private CityWeatherInfoCashValue GetWeatherInfoCash(string cityName)
        {
            return new CityWeatherInfoCashValue(_mainDataSource.GetWeatherInfo(cityName));
        }

        /// <summary>
        /// Класс для хранения результата кэширования
        /// </summary>
        private class DataProviderInfoCashValue
        {

            public DataProviderInfoCashValue(IDataProviderInfo[] infoList)
            {
                if (infoList == null) throw new ArgumentNullException(nameof(infoList));
                InfoListValue = infoList;
                TimeStamp = TimeProvider.Current.UtcNow;
            }

            public IDataProviderInfo[] InfoListValue { get; }
            public DateTime TimeStamp { get; }
        }

        /// <summary>
        /// Класс для хранения результата кэширования
        /// </summary>
        private class CityWeatherInfoCashValue
        {
            public CityWeatherInfoCashValue(ICityWeatherInfo weather)
            {
                if (weather == null) throw new ArgumentNullException(nameof(weather));
                Weather = weather;
                TimeStamp = TimeProvider.Current.UtcNow;
            }

            public ICityWeatherInfo Weather { get; }
            public DateTime TimeStamp { get; }
        }

    }
}

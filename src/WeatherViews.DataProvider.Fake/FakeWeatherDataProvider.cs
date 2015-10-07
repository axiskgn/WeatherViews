using System;
using WeatherViews.Common.DataProvider;
using WeatherViews.Common.DataProvider.BaseEntity;

namespace WeatherViews.DataProvider.Fake
{
    public class FakeWeatherDataProvider: IWeatherDataProvider
    {
        private static readonly Random _random = new Random();

        public FakeWeatherDataProvider(IDataProviderInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            Info = info;
        }

        public IDataProviderInfo Info { get; private set; }

        public IWeatherData GetWeatherData(IToponymData Toponym)
        {
            return new FakeWeatherData(_random);
        }

        private class FakeWeatherData: IWeatherData
        {
            //private static readonly string[] WindDirectionConsts = { "Юг", "Север", "Запад", "Восток" };

            private static readonly string[] PrecipitationConsts = { "Ясно", "Слабый дождь", "Сильный дождь", "Град" };

            public FakeWeatherData(Random random)
            {
                Temperature = _random.Next(500)/(double)10;
                WindDirection = _random.Next(360);//WindDirectionConsts[_random.Next(WindDirectionConsts.Length)];
                Condition = PrecipitationConsts[_random.Next(PrecipitationConsts.Length)];
                WindSpeed = _random.Next(50);
                Humidity = _random.Next(100);
                Pressure = _random.Next(100)/(double) 10 + 700;
            }

            public double Temperature { get; }
            public int WindDirection { get; }
            public int WindSpeed { get; }
            public string Condition { get; }
            public int Humidity { get; }
            public double Pressure { get; }
        }
    }
}
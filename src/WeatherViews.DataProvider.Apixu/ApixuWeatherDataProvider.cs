using APIXULib;
using WeatherViews.Common.DataProvider;
using WeatherViews.Common.DataProvider.BaseEntity;

namespace WeatherViews.DataProvider.Apixu
{
    public class ApixuWeatherDataProvider: IWeatherDataProvider
    {
        private readonly string _apiKey;
        private readonly Repository _repository;

        public ApixuWeatherDataProvider(string apiKey)
        {
            _apiKey = apiKey;
            _repository = new Repository();
            Info = new ApixuDataProviderInfo();
        }

        public IDataProviderInfo Info { get; }

        public IWeatherData GetWeatherData(IToponymData toponym)
        {
            var data = _repository.GetWeatherDataByLatLong(_apiKey, 
                toponym.Latitude.ToString("F2").Replace(',', '.'), 
                toponym.Longitude.ToString("F2").Replace(',', '.'));

            if (data == null)
            {
                throw new NotFoundWeatherDataException();
            }

            return new ApixuWeatherData(data);
        }

        private class ApixuWeatherData : IWeatherData
        {
            public ApixuWeatherData(WeatherModel data)
            {
                Temperature = data.current.temp_c;
                WindDirection = data.current.wind_degree;
                WindSpeed = (int)data.current.wind_kph;
                Condition = data.current.condition.text;
                Humidity = data.current.humidity;
                Pressure = data.current.pressure_mb / 1.3332239;
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

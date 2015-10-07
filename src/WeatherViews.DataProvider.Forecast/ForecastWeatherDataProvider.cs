using ForecastIO;
using WeatherViews.Common.DataProvider;
using WeatherViews.Common.DataProvider.BaseEntity;

namespace WeatherViews.DataProvider.Forecast
{
    public class ForecastWeatherDataProvider: IWeatherDataProvider
    {
        private readonly string _apiKey;

        public ForecastWeatherDataProvider(string apiKey)
        {
            _apiKey = apiKey;
            Info = new ForecastDataProviderInfo();
        }

        public IDataProviderInfo Info { get; }

        public IWeatherData GetWeatherData(IToponymData toponym)
        {
            var request = new ForecastIORequest(_apiKey, (float)toponym.Latitude, (float)toponym.Longitude, Unit.si);
            var response = request.Get();
            return new ForecastWeatherData(response);
        }

        private class ForecastWeatherData : IWeatherData
        {
            public ForecastWeatherData(ForecastIOResponse response)
            {
                Temperature = response.currently.temperature;
                WindDirection = (int)response.currently.windBearing;
                WindSpeed = (int)response.currently.windSpeed;
                Condition = response.currently.summary;
                Humidity = (int)(response.currently.humidity*100);
                Pressure = response.currently.pressure/ 1.3332239;
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
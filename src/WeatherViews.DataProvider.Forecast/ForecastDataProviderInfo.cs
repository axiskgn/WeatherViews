using WeatherViews.Common.DataProvider;

namespace WeatherViews.DataProvider.Forecast
{
    public class ForecastDataProviderInfo: IDataProviderInfo
    {
        public string Name { get { return "Forecast"; } }
        public string MainPageUrl { get { return "https://developer.forecast.io/"; } }
    }
}

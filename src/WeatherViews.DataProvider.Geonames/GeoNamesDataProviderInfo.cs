using WeatherViews.Common.DataProvider;

namespace WeatherViews.DataProvider.Geonames
{
    internal class GeoNamesDataProviderInfo: IDataProviderInfo
    {
        public string Name { get { return "Geonames.org"; } }
        public string MainPageUrl { get { return "http://www.geonames.org/"; } }
    }
}

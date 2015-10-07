using WeatherViews.Common.DataProvider;

namespace WeatherViews.DataProvider.Apixu
{
    public class ApixuDataProviderInfo: IDataProviderInfo
    {
        public string Name { get { return "Apixu"; } }
        public string MainPageUrl { get { return "http://www.apixu.com"; } }
    }
}

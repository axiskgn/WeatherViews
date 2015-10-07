using WeatherViews.Common.DataProvider;

namespace WeatherViews.DataProvider.Fake
{
    public class FakeDataProviderInfo: IDataProviderInfo
    {
        public FakeDataProviderInfo(string name, string mainPageUrl)
        {
            Name = name;
            MainPageUrl = mainPageUrl;
        }

        public string Name { get; }
        public string MainPageUrl { get; }
    }
}

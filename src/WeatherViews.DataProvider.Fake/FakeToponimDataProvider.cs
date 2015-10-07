using System;
using WeatherViews.Common.DataProvider;
using WeatherViews.Common.DataProvider.BaseEntity;

namespace WeatherViews.DataProvider.Fake
{
    public class FakeToponymDataProvider: IToponymDataProvider
    {
        public FakeToponymDataProvider(IDataProviderInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            Info = info;
        }

        public IDataProviderInfo Info { get; private set; }

        public IToponymData FindByName(string name)
        {
            return new FakeToponymData(name);
        }

        private class FakeToponymData : IToponymData
        {
            public FakeToponymData(string name)
            {
                Name = name;
                ToponymType = ToponymTypeEnum.City;
                ToponymName = name;
                Longitude = 0;
                Latitude = 0;
            }

            public string Name { get; }
            public ToponymTypeEnum ToponymType { get; }
            public string ToponymName { get; }
            public double Longitude { get; }
            public double Latitude { get; }
        }
    }
}

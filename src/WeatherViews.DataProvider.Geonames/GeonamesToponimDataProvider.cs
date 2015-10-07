using System;
using System.Diagnostics;
using System.Linq;
using NGeo.GeoNames;
using WeatherViews.Common.DataProvider;
using WeatherViews.Common.DataProvider.BaseEntity;

namespace WeatherViews.DataProvider.Geonames
{
    public class GeonamesToponymDataProvider:IToponymDataProvider
    {
        private readonly string _userName;

        public GeonamesToponymDataProvider(string userName)
        {
            _userName = userName;
            Info = new GeoNamesDataProviderInfo();
        }

        public IDataProviderInfo Info { get; }

        public IToponymData FindByName(string name)
        {
            DateTime time1 = DateTime.Now;
            using (var ngeoClient = new GeoNamesClient())
            {
                DateTime time2 = DateTime.Now;

                var rawData =
                    ngeoClient.Search(new SearchOptions(SearchType.Name, name) {UserName = _userName, MaxRows = 5});

                DateTime time3 = DateTime.Now;

                var result = rawData.Select(t=>new ToponymData(t)).FirstOrDefault(t => t.ToponymType==ToponymTypeEnum.City);
                DateTime time4 = DateTime.Now;

                Debug.WriteLine(String.Format("FindByName '{0}'  create {1} search {2} createObject {3}",
                    name,
                    (time2 - time1).TotalMilliseconds,
                    (time3 - time2).TotalMilliseconds,
                    (time4 - time3).TotalMilliseconds));

                if (result == null)
                {
                    throw new NotFoundToponymException();
                }

                return result;
            }

        }

        private class ToponymData : IToponymData
        {
            public ToponymData(Toponym toponym)
            {
                Name = toponym.Name;
                ToponymName = toponym.ToponymName;
                Longitude = toponym.Longitude;
                Latitude = toponym.Longitude;

                if (toponym.FeatureClassCode == "P")
                {
                    ToponymType = ToponymTypeEnum.City;
                }
                else
                {
                    ToponymType = ToponymTypeEnum.Other;
                }
            }

            public string Name { get; }
            public ToponymTypeEnum ToponymType { get; }
            public string ToponymName { get; }
            public double Longitude { get; }
            public double Latitude { get; }
        }
    }
}

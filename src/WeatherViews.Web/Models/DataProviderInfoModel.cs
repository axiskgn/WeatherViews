using System;
using WeatherViews.Common.DataProvider;

namespace WeatherViews.Web.Models
{
    public class DataProviderInfoModel
    {
        private readonly IDataProviderInfo _info;

        public DataProviderInfoModel(IDataProviderInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            _info = info;
        }

        public string Name { get { return _info.Name; } }

        public string MainPageUrl { get { return _info.MainPageUrl; } }
    }
}
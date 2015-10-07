using WeatherViews.Common.DataProvider;
using WeatherViews.Common.DataProvider.BaseEntity;

namespace WeatherViews.Common.Entity
{

    /// <summary>
    /// Информация о погоде от поставщика
    /// </summary>
    public interface IWeatherFromProviderInfo
    {

        /// <summary>
        /// Информация о поставщике данных
        /// </summary>
        IDataProviderInfo DataProviderInfo { get; } 

        /// <summary>
        /// Информация о погоде
        /// </summary>
        IWeatherData WeatherData { get; }
    }
}
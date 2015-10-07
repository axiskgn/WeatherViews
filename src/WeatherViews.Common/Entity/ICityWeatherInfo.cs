using WeatherViews.Common.DataProvider.BaseEntity;

namespace WeatherViews.Common.Entity
{

    /// <summary>
    /// Информация о погоде в городе
    /// </summary>
    public interface ICityWeatherInfo
    {
        /// <summary>
        /// Информация о городе
        /// </summary>
        IToponymData Toponym { get; } 

        /// <summary>
        /// Информация о погоде от разных поставщиков
        /// </summary>
        IWeatherFromProviderInfo[] WeatherFromProvider { get; }
    }
}
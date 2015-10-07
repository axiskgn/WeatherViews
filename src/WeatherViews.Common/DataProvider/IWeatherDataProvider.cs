using WeatherViews.Common.DataProvider.BaseEntity;

namespace WeatherViews.Common.DataProvider
{

    /// <summary>
    /// Провайдер данных о погоде
    /// </summary>
    public interface IWeatherDataProvider
    {

        /// <summary>
        /// Информация о поставщике данных
        /// </summary>
        IDataProviderInfo Info { get; }

        /// <summary>
        /// Получение данных
        /// </summary>
        /// <param name="toponym"></param>
        /// <returns></returns>
        IWeatherData GetWeatherData(IToponymData toponym);
    }
}
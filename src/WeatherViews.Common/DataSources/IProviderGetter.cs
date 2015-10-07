using WeatherViews.Common.DataProvider;

namespace WeatherViews.Common.DataSources
{

    /// <summary>
    /// Поставщик зарегистрированных провайдеров
    /// </summary>
    public interface IProviderGetter
    {

        /// <summary>
        /// Получаем провайдеры гео данных
        /// </summary>
        /// <returns></returns>
        IToponymDataProvider[] GetToponymProviders();

        /// <summary>
        /// Получаем провайдеры погодных данных
        /// </summary>
        /// <returns></returns>
        IWeatherDataProvider[] GetWeatherDataProviders();
    }
}
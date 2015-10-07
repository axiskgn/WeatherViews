using WeatherViews.Common.DataProvider;
using WeatherViews.Common.Entity;

namespace WeatherViews.Common.DataSources
{

    /// <summary>
    /// Источник данных для отображения
    /// </summary>
    public interface IWeatherViewDataSource
    {
        /// <summary>
        /// Получаем список доступных источников
        /// </summary>
        /// <returns></returns>
        IDataProviderInfo[] GetWeatherDataProviders();

        /// <summary>
        /// Получаем информацию о погоде в городе
        ///  </summary>
        /// <param name="cityName">Название города</param>
        /// <returns></returns>
        ICityWeatherInfo GetWeatherInfo(string cityName);

        /// <summary>
        /// Проверка на существование города
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        bool CheckName(string cityName);
    }
}
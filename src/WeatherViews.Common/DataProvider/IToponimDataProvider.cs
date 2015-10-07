using WeatherViews.Common.DataProvider.BaseEntity;

namespace WeatherViews.Common.DataProvider
{

    /// <summary>
    /// Провайдер гео данных
    /// </summary>
    public interface IToponymDataProvider
    {

        /// <summary>
        /// Информация о провайдере
        /// </summary>
        IDataProviderInfo Info { get; }

        /// <summary>
        /// Поиск гео объектов по названию
        /// </summary>
        /// <param name="name">название или его часть</param>
        /// <returns></returns>
        IToponymData FindByName(string name);
    }
}
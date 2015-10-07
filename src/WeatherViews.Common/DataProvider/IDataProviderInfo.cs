namespace WeatherViews.Common.DataProvider
{

    /// <summary>
    /// Информация о источнике данных
    /// </summary>
    public interface IDataProviderInfo
    {

        /// <summary>
        /// Название
        /// </summary>
        string Name { get; } 

        /// <summary>
        /// Сайт
        /// </summary>
        string MainPageUrl { get; }
    }
}
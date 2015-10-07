namespace WeatherViews.Common.DataProvider.BaseEntity
{

    /// <summary>
    /// Топоним
    /// </summary>
    public interface IToponymData
    {
        /// <summary>
        /// Название
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Тип
        /// </summary>
        ToponymTypeEnum ToponymType { get; }

        /// <summary>
        /// Географическое название (английское)
        /// </summary>
        string ToponymName { get; }

        /// <summary>
        /// Долгота
        /// </summary>
        double Longitude { get; }

        /// <summary>
        /// Широта
        /// </summary>
        double Latitude { get; }
    }
}
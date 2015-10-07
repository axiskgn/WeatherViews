namespace WeatherViews.Common.DataProvider.BaseEntity
{

    /// <summary>
    /// Информация о погоде
    /// </summary>
    public interface IWeatherData
    {

        /// <summary>
        /// Температура
        /// </summary>
        double Temperature { get;  }

        /// <summary>
        /// Направление ветра
        /// </summary>
        int WindDirection { get; }

        /// <summary>
        /// Скорость ветра
        /// </summary>
        int WindSpeed { get; }

        /// <summary>
        /// Состояние погоды
        /// </summary>
        string Condition { get; }

        /// <summary>
        /// Влажность
        /// </summary>
        int Humidity { get; }

        /// <summary>
        /// Давление
        /// </summary>
        double Pressure { get; }
    }
}
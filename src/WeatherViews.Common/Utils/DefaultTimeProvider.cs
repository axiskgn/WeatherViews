using System;

namespace WeatherViews.Common.Utils
{

    /// <summary>
    /// Провайдер времени по умолчанию
    /// возвращает текущее время
    /// </summary>
    public class DefaultTimeProvider : TimeProvider
    {
        public override DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }
    }
}
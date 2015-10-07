using System;

namespace WeatherViews.Common.Utils
{

    /// <summary>
    /// Провайдер времени, позволяет подменять текущее время. например для тестирования
    /// </summary>
    public abstract class TimeProvider
    {
        private static TimeProvider _current;

        static TimeProvider()
        {
            _current = new DefaultTimeProvider();
        }

        public static TimeProvider Current
        {
            get { return _current; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _current = value;
            }
        }

        public abstract DateTime UtcNow { get; }

        public static void ResetToDefault()
        {
            _current = new DefaultTimeProvider();
        }
    }
}

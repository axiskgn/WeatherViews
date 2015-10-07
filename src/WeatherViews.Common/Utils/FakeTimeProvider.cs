using System;

namespace WeatherViews.Common.Utils
{

    /// <summary>
    /// Отладочная версия провайдера времени
    /// </summary>
    public class FakeTimeProvider:TimeProvider
    {
        private DateTime _utcNow;

        public FakeTimeProvider()
        {
            _utcNow = DateTime.UtcNow;
        }

        public FakeTimeProvider(DateTime firstTime)
        {
            _utcNow = firstTime;
        }

        public void SetTime(DateTime newTime)
        {
            _utcNow = newTime;
        }

        public override DateTime UtcNow
        {
            get { return _utcNow; }
        }
    }
}

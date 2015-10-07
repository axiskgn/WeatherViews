using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WeatherViews.Common.Entity;

namespace WeatherViews.Web.Models
{

    /// <summary>
    /// Модель погоды в городе
    /// </summary>
    public class CityWeatherModel
    {
        private readonly ICityWeatherInfo _info;
        private readonly WeatherFromProviderModel[] _weather;

        public CityWeatherModel(ICityWeatherInfo info, string originalName)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            _info = info;
            _weather = _info.WeatherFromProvider.Select(t => new WeatherFromProviderModel(t)).ToArray();
            OriginalName = originalName;
        }

        [Display(Name = "Город")]
        public string CityName
        {
            get { return _info.Toponym.Name; }
        }

        public string OriginalName { get; private set; }

        public WeatherFromProviderModel[] Weather
        {
            get { return (WeatherFromProviderModel[]) _weather.Clone(); }
        }
    }
}
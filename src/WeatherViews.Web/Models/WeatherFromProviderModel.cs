using System;
using System.ComponentModel.DataAnnotations;
using WeatherViews.Common.Entity;

namespace WeatherViews.Web.Models
{
    public class WeatherFromProviderModel
    {
        private readonly IWeatherFromProviderInfo _weatherFromProvider;

        public WeatherFromProviderModel(IWeatherFromProviderInfo weatherFromProvider)
        {
            if (weatherFromProvider == null) throw new ArgumentNullException(nameof(weatherFromProvider));
            _weatherFromProvider = weatherFromProvider;
        }

        public string ProviderName
        {
            get { return _weatherFromProvider.DataProviderInfo.Name; }
        }

        [Display(Name = "Температура град")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public double Temperature
        {
            get { return _weatherFromProvider.WeatherData.Temperature; }
        }

        [Display(Name = "Направление ветра град.")]
        public int WindDirection
        {
            get { return _weatherFromProvider.WeatherData.WindDirection; }
        }

        [Display(Name = "Скорость ветра м/с")]
        public int WindSpeed
        {
            get { return _weatherFromProvider.WeatherData.WindSpeed; }
        }

        [Display(Name = "Состояние")]
        public string Condition
        {
            get { return _weatherFromProvider.WeatherData.Condition; }
        }

        [Display(Name = "Влажность %")]
        public int Humidity
        {
            get { return _weatherFromProvider.WeatherData.Humidity; }
        }

        [Display(Name = "Давление мм рт.ст.")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public double Pressure
        {
            get { return _weatherFromProvider.WeatherData.Pressure; }
        }
    }
}
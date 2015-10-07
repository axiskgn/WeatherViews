using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WeatherViews.Common.DataSources;
using WeatherViews.Web.Models;

namespace WeatherViews.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherViewDataSource _weatherViewDataSource;

        public HomeController(IWeatherViewDataSource weatherViewDataSource)
        {
            if (weatherViewDataSource == null) throw new ArgumentNullException(nameof(weatherViewDataSource));
            _weatherViewDataSource = weatherViewDataSource;
        }

        // GET: Home
        public ActionResult Index()
        {
            var sources = _weatherViewDataSource.GetWeatherDataProviders().Select(t=>new DataProviderInfoModel(t)).ToArray();
            return View(sources);
        }

        public PartialViewResult GetWeatherDate()
        {

            var locationList = GetLocationList();

            return CreateViewForDate(locationList);
        }

        public PartialViewResult AddLocation(string newLocationName)
        {
            var locationList = GetLocationList();
            if (!string.IsNullOrEmpty(newLocationName))
            {
                if (!locationList.Contains(newLocationName)&& _weatherViewDataSource.CheckName(newLocationName))
                {
                    locationList.Add(newLocationName);
                    SetLocationList(locationList);
                }
            }
            return CreateViewForDate(locationList);
        }

        public PartialViewResult DeleteLocation(string locationName)
        {
            var locationList = GetLocationList();
            if (!string.IsNullOrEmpty(locationName))
            {
                if (locationList.Contains(locationName))
                {
                    locationList.Remove(locationName);
                    SetLocationList(locationList);
                }
            }
            return CreateViewForDate(locationList);
        }

        private PartialViewResult CreateViewForDate(IEnumerable<string> locationList)
        {
            var list = locationList.Select(item => new CityWeatherModel(_weatherViewDataSource.GetWeatherInfo(item), item)).ToArray();
            return PartialView("GetWeatherDate", list);
        }

        private void SetLocationList(List<string> locationList)
        {
            var stringForSave = new StringBuilder();
            locationList.ForEach(s=>stringForSave.AppendFormat("{0}#",s));
            var bytes = Encoding.GetEncoding("Windows-1251").GetBytes(stringForSave.ToString());
            var cookie = new HttpCookie("Locations",Convert.ToBase64String(bytes)) {Expires = DateTime.Now.AddDays(10)};
            Response.SetCookie(cookie);
        }

        private List<string> GetLocationList()
        {
            var locationList = new List<string>();

            var cookie = Request.Cookies["Locations"];
            if (cookie != null)
            {
                var bytes = Convert.FromBase64String(cookie.Value);
                var locations = Encoding.GetEncoding("Windows-1251").GetString(bytes);
                locationList.AddRange(locations.Split('#').Where(t=>!string.IsNullOrEmpty(t)));
            }
            else
            {
                locationList.AddRange(new[] { "Курган", "Москва" });
            }
            return locationList;
        }
    }
}
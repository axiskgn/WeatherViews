using System;
using System.Linq;
using APIXULib;
using ForecastIO;
using NGeo.GeoNames;

namespace WeatherViews.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            double latitude;
            double longitude;

            GeoNamesTest(out latitude, out longitude);

            var repository = new Repository();
            // f97bbf791c4b4c1798295533152409

            //var data = repository.GetWeatherData("f97bbf791c4b4c1798295533152409", GetBy.CityName, "Kurgan");
            var data = repository.GetWeatherDataByLatLong("f97bbf791c4b4c1798295533152409", latitude.ToString("F2").Replace(',','.'), longitude.ToString("F2").Replace(',', '.'));

            Console.WriteLine(" {0} {1} {2}", data.location.name, data.location.country, data.current.temp_c);

            //ForecastTest(latitude, longitude);

            Console.ReadLine();
        }

        private static void ForecastTest(double latitude, double longitude)
        {
            var request = new ForecastIORequest("301feb278328af0b36ee03aa2f184c58", (float) latitude, (float) longitude, Unit.si);
            var response = request.Get();

            Console.WriteLine(response.currently.temperature);
        }

        private static void GeoNamesTest(out double latitude, out double longitude)
        {
            using (var ngeoClient = new GeoNamesClient())
            {
                var result = ngeoClient.Search(new SearchOptions(SearchType.Name, "Курган") {UserName = "axiskgn", MaxRows = 5});

                foreach (var toponym in result /*.Where(t=>t.FeatureCode=="PPLA")*/)
                {
                    Console.WriteLine(toponym.Name + " " + toponym.GeoNameId + " " + toponym.FeatureCode + " " +
                                      toponym.Population + " " + toponym.FeatureClassCode + " " + toponym.ToponymName);
                    //toponym.Latitude + toponym.Longitude
                }

                latitude = result.First().Latitude;
                longitude = result.First().Longitude;

            }
        }
    }
}

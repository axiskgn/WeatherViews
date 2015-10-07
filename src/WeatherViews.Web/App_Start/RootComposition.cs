using Ninject;
using WeatherViews.Common.DataProvider;
using WeatherViews.Common.DataSources;
using WeatherViews.DataProvider.Apixu;
using WeatherViews.DataProvider.Fake;
using WeatherViews.DataProvider.Forecast;
using WeatherViews.DataProvider.Geonames;

namespace WeatherViews.Web.App_Start
{

    /// <summary>
    /// Точка композиции
    /// При необходимости этот класс можно локализировать (вынести в отдельную сборку) 
    /// или написать тут интерпритатор конфига (анализ конфига и регистрация нужных классов)
    /// Автоматическая регистрация зависимостей имхо зло :)
    /// </summary>
    public static class RootComposition
    {
        public static void Register(IKernel kernel)
        {
            //Для отладки, что бы не наматывать данные с бесплатных аккаунтов
            var debug = false;

            if (debug)
            {
                kernel.Bind<IToponymDataProvider>()
                    .ToMethod(
                        context =>
                            new FakeToponymDataProvider(new FakeDataProviderInfo("Geonames", "http://www.geonames.org/")))
                    .InSingletonScope();

                kernel.Bind<IWeatherDataProvider>()
                    .ToMethod(
                        context =>
                            new FakeWeatherDataProvider(new FakeDataProviderInfo("Yahho",
                                "https://developer.yahoo.com/weather/")))
                    .InSingletonScope();

                kernel.Bind<IWeatherDataProvider>()
                    .ToMethod(
                        context => new FakeWeatherDataProvider(new FakeDataProviderInfo("apixu", "http://www.apixu.com")))
                    .InSingletonScope();
            }
            else
            {
                kernel.Bind<IToponymDataProvider>()
                    .ToMethod(context => new GeonamesToponymDataProvider("axiskgn"))
                    .InSingletonScope();

                kernel.Bind<IWeatherDataProvider>()
                    .ToMethod(
                        context =>
                            new ForecastWeatherDataProvider("301feb278328af0b36ee03aa2f184c58"))
                    .InSingletonScope();

                kernel.Bind<IWeatherDataProvider>()
                    .ToMethod(
                        context =>
                            new ApixuWeatherDataProvider("f97bbf791c4b4c1798295533152409"))
                    .InSingletonScope();
            }

            kernel.Bind<IProviderGetter>().To<NinjectProviderGetter>();

            kernel.Bind<IWeatherViewDataSource>()
                .ToMethod(
                    context =>
                        new CashedResultWeatherViewDataSource(
                            new DefaultWeatherViewDataSource(context.Kernel.Get<IProviderGetter>()), 100));

        }
    }
}
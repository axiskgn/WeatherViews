using System;
using System.Linq;
using Ninject;
using WeatherViews.Common.DataProvider;
using WeatherViews.Common.DataSources;

namespace WeatherViews.Web.App_Start
{
    public class NinjectProviderGetter: IProviderGetter
    {
        private readonly IKernel _kernel;

        public NinjectProviderGetter(IKernel kernel)
        {
            if (kernel == null) throw new ArgumentNullException(nameof(kernel));
            _kernel = kernel;
        }

        public IToponymDataProvider[] GetToponymProviders()
        {
            return _kernel.GetAll<IToponymDataProvider>().ToArray();
        }

        public IWeatherDataProvider[] GetWeatherDataProviders()
        {
            return _kernel.GetAll<IWeatherDataProvider>().ToArray();
        }
    }
}
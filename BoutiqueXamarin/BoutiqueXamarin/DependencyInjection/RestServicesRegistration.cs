using System;
using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Container;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarinCommon.Models.Interfaces.Configuration;
using Functional.FunctionalExtensions.Sync;
using Prism.Ioc;
using Prism.Unity;
using Unity;

namespace BoutiqueXamarin.DependencyInjection
{
    /// <summary>
    /// Регистрация сервисов обмена данными
    /// </summary>
    public static class RestServicesRegistration
    {
        /// <summary>
        /// Регистрация сервисов обмена данными
        /// </summary>
        public static void RegisterServices(IBoutiqueContainer container) =>
            container.Resolve<IXamarinConfigurationDomain>().
            HostConfiguration.
            Void(hostConfig => RegisterApiServices(container, HttpClientFactory.GetRestClient(hostConfig))).
            Void(_ => RegisterRestServices(container));

        /// <summary>
        /// Регистрация сервисов обмена данными
        /// </summary>
        private static void RegisterApiServices(IBoutiqueContainer container, HttpClient httpClient)
        {
            container.Register<IGenderApiService>(service => new GenderApiService(httpClient));
            container.Register<ICategoryApiService>(service => new CategoryApiService(httpClient));
            container.Register<IClothesTypeApiService>(service => new ClothesTypeApiService(httpClient));
            container.Register<IColorApiService>(service => new ColorApiService(httpClient));
            container.Register<ISizeApiService>(service => new SizeApiService(httpClient));
            container.Register<ISizeGroupApiService>(service => new SizeGroupApiService(httpClient));
            container.Register<IClothesApiService>(service => new ClothesApiService(httpClient));
        }

        /// <summary>
        /// Регистрация сервисов обмена данными
        /// </summary>
        private static void RegisterRestServices(IBoutiqueContainer container)
        {
            container.Register<IGenderRestService, GenderRestService>();
            container.Register<ICategoryRestService, CategoryRestService>();
            container.Register<IClothesTypeRestService, ClothesTypeRestService>();
            container.Register<IColorRestService, ColorRestService>();
            container.Register<ISizeRestService, SizeRestService>();
            container.Register<ISizeGroupRestService, SizeGroupRestService>();
            container.Register<IClothesRestService, ClothesRestService>();
        }

      
    }
}
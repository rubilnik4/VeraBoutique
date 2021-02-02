using System;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Container;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Connection;
using BoutiqueXamarin.Infrastructure.Implementations;
using BoutiqueXamarin.Infrastructure.Interfaces.Configuration;
using BoutiqueXamarin.Models.Interfaces.Configuration;
using Functional.FunctionalExtensions.Sync;
using Prism.Ioc;
using Prism.Unity;
using RestSharp;
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
            Void(hostConfig => RegisterApiServices(container, RestSharpFactory.GetRestClient(hostConfig))).
            Void(_ => RegisterRestServices(container));

        /// <summary>
        /// Регистрация сервисов обмена данными
        /// </summary>
        private static void RegisterApiServices(IBoutiqueContainer container, IRestClient restClient)
        {
            container.Register<IGenderApiService>(service => new GenderApiService(restClient));
            container.Register<ICategoryApiService>(service => new CategoryApiService(restClient));
            container.Register<IClothesTypeApiService>(service => new ClothesTypeApiService(restClient));
            container.Register<IColorApiService>(service => new ColorApiService(restClient));
            container.Register<ISizeApiService>(service => new SizeApiService(restClient));
            container.Register<ISizeGroupApiService>(service => new SizeGroupApiService(restClient));
            container.Register<IClothesApiService>(service => new ClothesApiService(restClient));
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
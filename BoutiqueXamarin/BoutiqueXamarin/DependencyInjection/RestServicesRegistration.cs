using System;
using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Container;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueDTO.Factory.HttpClients;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorize;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Interfaces.RestClients;
using BoutiqueXamarinCommon.Models.Interfaces.Configuration;
using ResultFunctional.FunctionalExtensions.Sync;
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
        public static void RegisterServices(IBoutiqueContainer container, IXamarinConfigurationDomain configuration) =>
            container.
            Void(_ => RegisterRestClient(container, configuration)).
            Void(_ => RegisterRestServices(container));

        /// <summary>
        /// Регистрация сервисов обмена данными
        /// </summary>
        private static void RegisterRestClient(IBoutiqueContainer container, IXamarinConfigurationDomain configuration) =>
            configuration.HostConfiguration.
            Void(hostConfig => container.Register(service => HttpClientFactory.GetRestClient(hostConfig)));

        /// <summary>
        /// Регистрация сервисов обмена данными
        /// </summary>
        private static void RegisterRestServices(IBoutiqueContainer container)
        {
            container.Register<IAuthorizeRestService, AuthorizeRestService>();
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
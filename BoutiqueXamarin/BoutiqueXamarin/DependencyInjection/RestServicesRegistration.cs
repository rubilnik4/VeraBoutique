using System;
using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Container;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueDTO.Factory.HttpClients;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorize;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.RestClients;
using BoutiqueDTO.Models.Interfaces.RestClients;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using BoutiqueXamarinCommon.Models.Interfaces.Configuration;
using ResultFunctional.FunctionalExtensions.Sync;
using Prism.Ioc;
using Prism.Unity;
using ResultFunctional.FunctionalExtensions.Async;
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
            Void(_ => RegisterRestClient(container, configuration.HostConfiguration)).
            Void(_ => RegisterRestServices(container));

        /// <summary>
        /// Регистрация сервисов обмена данными
        /// </summary>
        private static void RegisterRestClient(IBoutiqueContainer container, IHostConfigurationDomain hostConfiguration) =>
            container.Register(service => GetRestJwtHttpClient(service.Resolve<HttpClientHandler>(), hostConfiguration, 
                                                               service.Resolve<ILoginStore>()));

        /// <summary>
        /// Получить http клиент с авторизацией
        /// </summary>
        private static IRestHttpClient GetRestJwtHttpClient(HttpClientHandler httpClientHandler, IHostConfigurationDomain hostConfiguration,
                                                            ILoginStore loginStore) =>
            new RestJwtHttpClient(httpClientHandler, hostConfiguration.Host, hostConfiguration.TimeOut, loginStore.GetTokenValue);

        /// <summary>
        /// Регистрация сервисов обмена данными
        /// </summary>
        private static void RegisterRestServices(IBoutiqueContainer container)
        {
            container.Register<IAuthorizeRestService, AuthorizeRestService>();
            container.Register<IUserRestService, UserRestService>();
            container.Register<IProfileRestService, ProfileRestService>();
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
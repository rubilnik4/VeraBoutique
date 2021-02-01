using System;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Connection;
using BoutiqueXamarin.Infrastructure.Implementations;
using Prism.Ioc;
using RestSharp;

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
        public static async void RegisterServices(IContainerRegistry containerRegistry)
        {
            RegisterRestServices(containerRegistry);
            RegisterApiServices(containerRegistry);
        }

        /// <summary>
        /// Регистрация сервисов обмена данными
        /// </summary>
        private static void RegisterRestServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IGenderRestService, GenderRestService>();
            containerRegistry.Register<ICategoryRestService, CategoryRestService>();
            containerRegistry.Register<IClothesTypeRestService, ClothesTypeRestService>();
            containerRegistry.Register<IColorRestService, ColorRestService>();
            containerRegistry.Register<ISizeRestService, SizeRestService>();
            containerRegistry.Register<ISizeGroupRestService, SizeGroupRestService>();
            containerRegistry.Register<IClothesRestService, ClothesRestService>();
        }

        /// <summary>
        /// Регистрация сервисов обмена данными
        /// </summary>
        private static async Task RegisterApiServices(IContainerRegistry containerRegistry)
        {
            var hostConnection = new HostConnection(new Uri("https://10.0.2.2:5001/"), TimeSpan.FromSeconds(5), true);
            var restClient = RestSharpFactory.GetRestClient(hostConnection);

            containerRegistry.Register<IGenderApiService>(async (service) => await GetRestClient(service));
            containerRegistry.Register<ICategoryApiService>(service => new CategoryApiService(restClient));
            containerRegistry.Register<IClothesTypeApiService>(service => new ClothesTypeApiService(restClient));
            containerRegistry.Register<IColorApiService>(service => new ColorApiService(restClient));
            containerRegistry.Register<ISizeApiService>(service => new SizeApiService(restClient));
            containerRegistry.Register<ISizeGroupApiService>(service => new SizeGroupApiService(restClient));
            containerRegistry.Register<IClothesApiService>(service => new ClothesApiService(restClient));
        }

        private static Task<IRestClient> GetRestClient(IContainerProvider containerProvider) =>
            containerProvider.Resolve()
    }
}
using System;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Connection;
using BoutiqueXamarin.Infrastructure.Implementations;
using Prism.Ioc;

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
        public static void RegisterServices(IContainerRegistry containerRegistry)
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
        private static void RegisterApiServices(IContainerRegistry containerRegistry)
        {
            var hostConnection = new HostConnection(new Uri("https://localhost:5001/"), TimeSpan.FromSeconds(5));
            var restClient = RestSharpFactory.GetRestClient(hostConnection);

            containerRegistry.Register<IGenderApiService>(service => new GenderApiService(restClient));
            containerRegistry.Register<ICategoryApiService>(service => new CategoryApiService(restClient));
            containerRegistry.Register<IClothesTypeApiService>(service => new ClothesTypeApiService(restClient));
            containerRegistry.Register<IColorApiService>(service => new ColorApiService(restClient));
            containerRegistry.Register<ISizeApiService>(service => new SizeApiService(restClient));
            containerRegistry.Register<ISizeGroupApiService>(service => new SizeGroupApiService(restClient));
            containerRegistry.Register<IClothesApiService>(service => new ClothesApiService(restClient));
        }
    }
}
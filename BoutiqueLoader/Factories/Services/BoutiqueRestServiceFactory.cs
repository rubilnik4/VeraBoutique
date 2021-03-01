using System.Drawing;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Authorization;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.CategoryTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.GenderTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Authorization;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorization;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorization;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueLoader.Factories.Configuration;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;
using RestSharp;

namespace BoutiqueLoader.Factories.Services
{
    /// <summary>
    /// Фабрика создания сервисов для получения и загрузки данных Api
    /// </summary>
    public static class BoutiqueRestServiceFactory
    {
        /// <summary>
        /// Клиент для подключения к сервису одежды
        /// </summary>
        public static async Task<IResultValue<IRestClient>> GetBoutiqueRestClient(IBoutiqueLogger boutiqueLogger) =>
            await LoaderConfigurationFactory.GetConfiguration(boutiqueLogger).
            ResultValueOkTaskAsync(config => RestSharpFactory.GetRestClient(config.HostConfiguration));

        /// <summary>
        /// Клиент для подключения к сервису одежды
        /// </summary>
        public static async Task<IResultValue<IRestClient>> GetBoutiqueRestClient(string jwtToken, IBoutiqueLogger boutiqueLogger) =>
            await LoaderConfigurationFactory.GetConfiguration(boutiqueLogger).
            ResultValueOkTaskAsync(config => RestSharpFactory.GetRestClient(config.HostConfiguration, jwtToken));

        /// <summary>
        ///  Получить сервис авторизации
        /// </summary>
        public static IAuthorizeRestService GetAuthorizeRestService(IRestClient restClient) =>
             new AuthorizeRestService(new AuthorizeApiService(restClient), new AuthorizeTransferConverter());

        /// <summary>
        /// Получить сервис типа пола
        /// </summary>
        public static IGenderRestService GetGenderRestService(IRestClient restClient) =>
             new GenderRestService(new GenderApiService(restClient), new GenderTransferConverter());

        /// <summary>
        /// Получить сервис категорий одежды
        /// </summary>
        public static ICategoryRestService GetCategoryRestService(IRestClient restClient) =>
             new CategoryRestService(new CategoryApiService(restClient), new CategoryTransferConverter());

        /// <summary>
        /// Получить сервис категорий одежды
        /// </summary>
        public static IColorRestService GetColorRestService(IRestClient restClient) =>
             new ColorRestService(new ColorApiService(restClient), new ColorTransferConverter());

        /// <summary>
        /// Получить сервис типа одежды
        /// </summary>
        public static IClothesTypeRestService GetClothesTypeRestService(IRestClient restClient) =>
             new ClothesTypeRestService(new ClothesTypeApiService(restClient), 
                                        new ClothesTypeMainTransferConverter(new CategoryTransferConverter()));

        /// <summary>
        /// Получить сервис размера одежды
        /// </summary>
        public static ISizeRestService GetSizeRestService(IRestClient restClient) =>
             new SizeRestService(new SizeApiService(restClient), new SizeTransferConverter());

        /// <summary>
        /// Получить сервис размера одежды
        /// </summary>
        public static ISizeGroupRestService GetSizeGroupRestService(IRestClient restClient) =>
             new SizeGroupRestService(new SizeGroupApiService(restClient),
                                      new SizeGroupMainTransferConverter(new SizeTransferConverter()));

        /// <summary>
        /// Получить сервис одежды
        /// </summary>
        public static IClothesRestService GetClothesRestService(IRestClient restClient) =>
             new ClothesRestService(new ClothesApiService(restClient),
                                    new ClothesMainTransferConverter(new GenderTransferConverter(),
                                                                 new ClothesTypeTransferConverter(),
                                                                 new ColorTransferConverter(),
                                                                 new SizeGroupMainTransferConverter(new SizeTransferConverter())));
    }
}
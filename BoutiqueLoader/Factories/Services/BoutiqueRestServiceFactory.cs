using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueDTO.Factory.HttpClients;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Authorization;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.CategoryTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.GenderTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ImageConverters;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorization;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorization;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Interfaces.RestClients;
using BoutiqueLoader.Factories.Configuration;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;

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
        public static async Task<IResultValue<IRestHttpClient>> GetBoutiqueRestClient(IBoutiqueLogger boutiqueLogger) =>
            await LoaderConfigurationFactory.GetConfiguration(boutiqueLogger).
            ResultValueOkTaskAsync(config => HttpClientFactory.GetRestClient(config.HostConfiguration));

        /// <summary>
        /// Клиент для подключения к сервису одежды
        /// </summary>
        public static async Task<IResultValue<IRestHttpClient>> GetBoutiqueRestClient(string jwtToken, IBoutiqueLogger boutiqueLogger) =>
            await LoaderConfigurationFactory.GetConfiguration(boutiqueLogger).
            ResultValueOkTaskAsync(config => HttpClientFactory.GetRestClient(config.HostConfiguration, jwtToken));

        /// <summary>
        ///  Получить сервис авторизации
        /// </summary>
        public static IAuthorizeRestService GetAuthorizeRestService(IRestHttpClient restHttpClient) =>
             new AuthorizeRestService(restHttpClient, new AuthorizeTransferConverter());

        /// <summary>
        /// Получить сервис типа пола
        /// </summary>
        public static IGenderRestService GetGenderRestService(IRestHttpClient restHttpClient) =>
             new GenderRestService(restHttpClient, new GenderTransferConverter(),
                                   new ClothesTypeTransferConverter().
                                   Map(clothesTypeConverter => new CategoryClothesTypeTransferConverter(clothesTypeConverter)).
                                   Map(categoryConverter => new GenderCategoryTransferConverter(categoryConverter)));

        /// <summary>
        /// Получить сервис категорий одежды
        /// </summary>
        public static ICategoryRestService GetCategoryRestService(IRestHttpClient restHttpClient) =>
             new CategoryRestService(restHttpClient, new CategoryMainTransferConverter(new GenderTransferConverter()));

        /// <summary>
        /// Получить сервис категорий одежды
        /// </summary>
        public static IColorRestService GetColorRestService(IRestHttpClient restHttpClient) =>
             new ColorRestService(restHttpClient, new ColorTransferConverter());

        /// <summary>
        /// Получить сервис типа одежды
        /// </summary>
        public static IClothesTypeRestService GetClothesTypeRestService(IRestHttpClient restHttpClient) =>
             new ClothesTypeRestService(restHttpClient,
                                        new ClothesTypeMainTransferConverter(new CategoryTransferConverter()));

        /// <summary>
        /// Получить сервис размера одежды
        /// </summary>
        public static ISizeRestService GetSizeRestService(IRestHttpClient restHttpClient) =>
             new SizeRestService(restHttpClient, new SizeTransferConverter());

        /// <summary>
        /// Получить сервис размера одежды
        /// </summary>
        public static ISizeGroupRestService GetSizeGroupRestService(IRestHttpClient restHttpClient) =>
             new SizeGroupRestService(restHttpClient,
                                      new SizeGroupMainTransferConverter(new SizeTransferConverter()));

        /// <summary>
        /// Получить сервис одежды
        /// </summary>
        public static IClothesRestService GetClothesRestService(IRestHttpClient restHttpClient) =>
             new ClothesRestService(restHttpClient,
                                    new ClothesTransferConverter(),
                                    new ClothesDetailTransferConverter(new ColorTransferConverter(),
                                                                      new SizeGroupMainTransferConverter(new SizeTransferConverter())),
                                    new ClothesMainTransferConverter(new ClothesImageTransferConverter(),
                                                                     new GenderTransferConverter(),
                                                                     new ClothesTypeTransferConverter(),
                                                                     new ColorTransferConverter(),
                                                                     new SizeGroupMainTransferConverter(new SizeTransferConverter())),
                                    new ClothesImageTransferConverter());
    }
}
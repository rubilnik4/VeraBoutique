using System.Drawing;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Authorization;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Authorization;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorization;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorization;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueLoader.Factories.Connection;
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
        public static IResultValue<IRestClient> GetBoutiqueRestClient() =>
            BoutiqueConnection.BoutiqueHostConnection.
            ResultValueOk(RestSharpFactory.GetRestClient);

        /// <summary>
        /// Клиент для подключения к сервису одежды
        /// </summary>
        public static IResultValue<IRestClient> GetBoutiqueRestClient(string jwtToken) =>
            BoutiqueConnection.BoutiqueHostConnection.
            ResultValueOk(hostConnection => RestSharpFactory.GetRestClient(hostConnection, jwtToken));

        /// <summary>
        ///  Получить сервис авторизации
        /// </summary>
        public static IAuthorizeRestService GetAuthorizeRestService(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
             new AuthorizeRestService(new AuthorizeApiService(restClient), new AuthorizeTransferConverter(), boutiqueLogger);

        /// <summary>
        /// Получить сервис типа пола
        /// </summary>
        public static IGenderRestService GetGenderRestService(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
             new GenderRestService(new GenderApiService(restClient), new GenderTransferConverter(), boutiqueLogger);

        /// <summary>
        /// Получить сервис категорий одежды
        /// </summary>
        public static ICategoryRestService GetCategoryRestService(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
             new CategoryRestService(new CategoryApiService(restClient), new CategoryTransferConverter(), boutiqueLogger);

        /// <summary>
        /// Получить сервис категорий одежды
        /// </summary>
        public static IColorRestService GetColorRestService(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
             new ColorRestService(new ColorApiService(restClient), new ColorTransferConverter(), boutiqueLogger);

        /// <summary>
        /// Получить сервис типа одежды
        /// </summary>
        public static IClothesTypeRestService GetClothesTypeRestService(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
             new ClothesTypeRestService(new ClothesTypeApiService(restClient), 
                                        new ClothesTypeTransferConverter(new CategoryTransferConverter(),
                                                                         new GenderTransferConverter()), 
                                        boutiqueLogger);

        /// <summary>
        /// Получить сервис размера одежды
        /// </summary>
        public static ISizeRestService GetSizeRestService(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
             new SizeRestService(new SizeApiService(restClient), new SizeTransferConverter(), boutiqueLogger);

        /// <summary>
        /// Получить сервис размера одежды
        /// </summary>
        public static ISizeGroupRestService GetSizeGroupRestService(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
             new SizeGroupRestService(new SizeGroupApiService(restClient),
                                      new SizeGroupTransferConverter(new SizeTransferConverter()),
                                      boutiqueLogger);

        /// <summary>
        /// Получить сервис одежды
        /// </summary>
        public static IClothesRestService GetClothesRestService(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
             new ClothesRestService(new ClothesApiService(restClient),
                                    new ClothesTransferConverter(new GenderTransferConverter(),
                                                                 new ClothesTypeShortTransferConverter(),
                                                                 new ColorTransferConverter(),
                                                                 new SizeGroupTransferConverter(new SizeTransferConverter())),
                                    boutiqueLogger);
    }
}
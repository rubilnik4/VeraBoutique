using System.Drawing;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes;
using BoutiqueDTO.Models.Interfaces.Connection;
using BoutiquePrerequisites.Factories.Connection;
using BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase.Services.Clothes;
using BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Services.Clothes;
using BoutiquePrerequisites.Infrastructure.Interfaces.Logger;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;
using RestSharp;

namespace BoutiquePrerequisites.Factories.Services
{
    /// <summary>
    /// Фабрика создания сервисов для получения и загрузки данных Api
    /// </summary>
    public static class RestServiceFactory
    {
        /// <summary>
        /// Клиент для подключения к сервису одежды
        /// </summary>
        public static IResultValue<IRestClient> BoutiqueRestClient =>
            BoutiqueConnection.BoutiqueHostConnection.
            ResultValueOk(RestSharpFactory.GetRestClient);
        
        /// <summary>
        /// Получить сервис типа пола
        /// </summary>
        public static IGenderRestService GetGenderRestService(IRestClient restClient, ILogger logger) =>
             new GenderRestService( new GenderApiService(restClient), new GenderTransferConverter(), logger);

        /// <summary>
        /// Получить сервис категорий одежды
        /// </summary>
        public static ICategoryRestService GetCategoryRestService(IRestClient restClient, ILogger logger) =>
             new CategoryRestService( new CategoryApiService(restClient), new CategoryTransferConverter(), logger);

        /// <summary>
        /// Получить сервис категорий одежды
        /// </summary>
        public static IColorRestService GetColorRestService(IRestClient restClient, ILogger logger) =>
             new ColorRestService(new ColorApiService(restClient), new ColorTransferConverter(), logger);

        /// <summary>
        /// Получить сервис типа одежды
        /// </summary>
        public static IClothesTypeRestService GetClothesTypeRestService(IRestClient restClient, ILogger logger) =>
             new ClothesTypeRestService(new ClothesTypeApiService(restClient), 
                                        new ClothesTypeTransferConverter(new CategoryTransferConverter(),
                                                                         new GenderTransferConverter()), 
                                        logger);

        /// <summary>
        /// Получить сервис размера одежды
        /// </summary>
        public static ISizeRestService GetSizeRestService(IRestClient restClient, ILogger logger) =>
             new SizeRestService(new SizeApiService(restClient), new SizeTransferConverter(), logger);

        /// <summary>
        /// Получить сервис размера одежды
        /// </summary>
        public static ISizeGroupRestService GetSizeGroupRestService(IRestClient restClient, ILogger logger) =>
             new SizeGroupRestService(new SizeGroupApiService(restClient),
                                      new SizeGroupTransferConverter(new SizeTransferConverter()),
                                      logger);
    }
}
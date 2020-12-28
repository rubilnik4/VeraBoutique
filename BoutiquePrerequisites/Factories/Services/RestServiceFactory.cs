using System.Drawing;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
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
             new GenderRestService(new GenderTransferConverter(), new GenderApiService(restClient), logger);

        /// <summary>
        /// Получить сервис категорий одежды
        /// </summary>
        public static ICategoryRestService GetCategoryRestService(IRestClient restClient, ILogger logger) =>
             new CategoryRestService(new CategoryTransferConverter(), new CategoryApiService(restClient), logger);

        /// <summary>
        /// Получить сервис категорий одежды
        /// </summary>
        public static IColorRestService GetColorRestService(IRestClient restClient, ILogger logger) =>
             new ColorRestService(new ColorTransferConverter(), new ColorApiService(restClient), logger);
    }
}
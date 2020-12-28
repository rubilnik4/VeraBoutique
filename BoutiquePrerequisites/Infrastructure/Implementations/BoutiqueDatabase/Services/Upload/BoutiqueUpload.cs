using System.Threading.Tasks;
using BoutiquePrerequisites.Factories.DatabaseInitialize.Boutique;
using BoutiquePrerequisites.Factories.Services;
using BoutiquePrerequisites.Infrastructure.Interfaces.Logger;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using RestSharp;

namespace BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase.Services.Upload
{
    /// <summary>
    /// Загрузка данных в базу
    /// </summary>
    public static class BoutiqueUpload
    {
        /// <summary>
        /// Загрузить данных в базу
        /// </summary>
        public static async Task<IResultError> UploadData(ILogger logger) =>
            await logger.Void(_ => logger.ShowMessage("Начало загрузки данных")).
            Map(_ => RestServiceFactory.BoutiqueRestClient).
            ResultValueBindErrorsOkAsync(restClient => GenderUpload(restClient, logger)).
            ResultValueBindErrorsOkBindAsync(restClient => CategoryUpload(restClient, logger)).
            ResultValueBindErrorsOkBindAsync(restClient => ColorUpload(restClient, logger)).
            Void(_ => logger.ShowMessage("Загрузка данных завершена"));

        /// <summary>
        /// Загрузить тип пола в базу
        /// </summary>
        private static async Task<IResultError> GenderUpload(IRestClient restClient, ILogger logger) =>
            await RestServiceFactory.GetGenderRestService(restClient, logger).
            Upload(GenderInitialize.Genders);

        /// <summary>
        /// Загрузить категории одежды в базу
        /// </summary>
        private static async Task<IResultError> CategoryUpload(IRestClient restClient, ILogger logger) =>
            await RestServiceFactory.GetCategoryRestService(restClient, logger).
            Upload(CategoryInitialize.Categories);

        /// <summary>
        /// Загрузить цвет одежды в базу
        /// </summary>
        private static async Task<IResultError> ColorUpload(IRestClient restClient, ILogger logger) =>
            await RestServiceFactory.GetColorRestService(restClient, logger).
            Upload(ColorInitialize.ColorClothes);
    }
}
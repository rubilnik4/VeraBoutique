using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiquePrerequisites.Factories.DatabaseInitialize.Boutique;
using BoutiquePrerequisites.Factories.Services;
using BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase.Services.Authorization;
using BoutiquePrerequisites.Infrastructure.Interfaces.Logger;
using Functional.FunctionalExtensions.Async;
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
        /// Авторизироваться и загрузить данные в базу
        /// </summary>
        public static async Task<IResultError> UploadAuthorizeData(ILogger logger) =>
            await BoutiqueAuthorizeService.AuthorizeJwt(logger).
            ResultValueBindErrorsOkBindAsync(jwtToken => UploadData(jwtToken, logger));

        /// <summary>
        /// Авторизироваться и загрузить данные в базу
        /// </summary>
        private static async Task<IResultError> UploadData(string jwtToken, ILogger logger) =>
            await logger.Void(_ => logger.ShowMessage("Начало загрузки данных")).
            Map(_ => BoutiqueRestServiceFactory.GetBoutiqueRestClient(jwtToken)).
            //ResultValueBindErrorsOkAsync(restClient => GenderUpload(restClient, logger)).
            //ResultValueBindErrorsOkBindAsync(restClient => CategoryUpload(restClient, logger)).
            //ResultValueBindErrorsOkBindAsync(restClient => ColorUpload(restClient, logger)).
            //ResultValueBindErrorsOkBindAsync(restClient => ClothesTypeUpload(restClient, logger)).
            //ResultValueBindErrorsOkBindAsync(restClient => SizeUpload(restClient, logger)).
            ResultValueBindErrorsOkAsync(restClient => SizeGroupUpload(restClient, logger)).
            VoidTaskAsync(_ => logger.ShowMessage("Загрузка данных завершена"));

        /// <summary>
        /// Загрузить тип пола в базу
        /// </summary>
        private static async Task<IResultError> GenderUpload(IRestClient restClient, ILogger logger) =>
            await BoutiqueRestServiceFactory.GetGenderRestService(restClient, logger).
            Upload(GenderInitialize.Genders);

        /// <summary>
        /// Загрузить категории одежды в базу
        /// </summary>
        private static async Task<IResultError> CategoryUpload(IRestClient restClient, ILogger logger) =>
            await BoutiqueRestServiceFactory.GetCategoryRestService(restClient, logger).
            Upload(CategoryInitialize.Categories);

        /// <summary>
        /// Загрузить цвет одежды в базу
        /// </summary>
        private static async Task<IResultError> ColorUpload(IRestClient restClient, ILogger logger) =>
            await BoutiqueRestServiceFactory.GetColorRestService(restClient, logger).
            Upload(ColorInitialize.ColorClothes);

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> ClothesTypeUpload(IRestClient restClient, ILogger logger) =>
            await BoutiqueRestServiceFactory.GetClothesTypeRestService(restClient, logger).
            Upload(ClothesTypeInitialize.ClothesTypes);

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> SizeUpload(IRestClient restClient, ILogger logger) =>
            await BoutiqueRestServiceFactory.GetSizeRestService(restClient, logger).
            Upload(SizeInitialize.Sizes);

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> SizeGroupUpload(IRestClient restClient, ILogger logger) =>
            await BoutiqueRestServiceFactory.GetSizeGroupRestService(restClient, logger).
            Upload(SizeGroupInitialize.SizeGroups);
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.CollectionExtensions;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base;
using BoutiqueLoader.Factories.DatabaseInitialize.Boutique;
using BoutiqueLoader.Factories.Services;
using BoutiqueLoader.Models.Enums;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;

namespace BoutiqueLoader.Infrastructure.Implementations.Services.Upload
{
    /// <summary>
    /// Загрузка данных
    /// </summary>
    public static class BoutiquePost
    {
        /// <summary>
        /// Длина пачек запросов
        /// </summary>
        private const int POST_CHUNK = 10;

        /// <summary>
        /// Загрузить данные в базу
        /// </summary>
        public static async Task<IResultError> PostData(HttpClient restClientBoutique, IBoutiqueLogger boutiqueLogger) =>
            await restClientBoutique.ToResultValue().
            Void(_ => boutiqueLogger.ShowMessage("Начало загрузки данных")).
            ResultValueBindErrorsOkAsync(restClient => GenderUpload(restClient, boutiqueLogger)).
            ResultValueBindErrorsOkBindAsync(restClient => CategoryUpload(restClient, boutiqueLogger)).
            ResultValueBindErrorsOkBindAsync(restClient => ColorUpload(restClient, boutiqueLogger)).
            ResultValueBindErrorsOkBindAsync(restClient => ClothesTypeUpload(restClient, boutiqueLogger)).
            ResultValueBindErrorsOkBindAsync(restClient => SizeUpload(restClient, boutiqueLogger)).
            ResultValueBindErrorsOkBindAsync(restClient => SizeGroupUpload(restClient, boutiqueLogger)).
            ResultValueBindErrorsOkBindAsync(restClient => ClothesUpload(restClient, boutiqueLogger)).
            VoidTaskAsync(_ => boutiqueLogger.ShowMessage("Загрузка данных завершена"));

        /// <summary>
        /// Загрузить тип пола в базу
        /// </summary>
        private static async Task<IResultError> GenderUpload(HttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetGenderRestService(restClient).
            MapAsync(service => ServicePostAction(service, GenderInitialize.Genders, boutiqueLogger));

        /// <summary>
        /// Загрузить категории одежды в базу
        /// </summary>
        private static async Task<IResultError> CategoryUpload(HttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetCategoryRestService(restClient).
            MapAsync(service => ServicePostAction(service, CategoryInitialize.Categories, boutiqueLogger));

        /// <summary>
        /// Загрузить цвет одежды в базу
        /// </summary>
        private static async Task<IResultError> ColorUpload(HttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetColorRestService(restClient).
            MapAsync(service => ServicePostAction(service, ColorInitialize.ColorClothes, boutiqueLogger));

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> ClothesTypeUpload(HttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetClothesTypeRestService(restClient).
            MapAsync(service => ServicePostAction(service, ClothesTypeInitialize.ClothesTypeMains, boutiqueLogger));

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> SizeUpload(HttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetSizeRestService(restClient).
            MapAsync(service => ServicePostAction(service, SizeInitialize.Sizes, boutiqueLogger));

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> SizeGroupUpload(HttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetSizeGroupRestService(restClient).
            MapAsync(service => ServicePostAction(service, SizeGroupInitialize.SizeGroupMains, boutiqueLogger));

        /// <summary>
        /// Загрузить одежду в базу
        /// </summary>
        private static async Task<IResultError> ClothesUpload(HttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetClothesRestService(restClient).
            MapAsync(service => ClothesInitialize.ClothesMains.SelectChunk(POST_CHUNK).
                                Select(clothesMains => ServicePostAction(service, clothesMains, boutiqueLogger)).
                                ToResultErrorsTaskAsync());

        /// <summary>
        /// Логгирование загрузки
        /// </summary>
        private static async Task<IResultError> ServicePostAction<TId, TDomain>(IRestServiceBase<TId, TDomain> restService,
                                                                                IEnumerable<TDomain> domains, IBoutiqueLogger boutiqueLogger)
            where TDomain : IDomainModel<TId>
            where TId : notnull =>
            await restService.PostAsync(domains).
            VoidTaskAsync(result => BoutiqueServiceLog.LogServiceAction<TId, TDomain>(result, boutiqueLogger, ServiceActionType.Post));
    }
}
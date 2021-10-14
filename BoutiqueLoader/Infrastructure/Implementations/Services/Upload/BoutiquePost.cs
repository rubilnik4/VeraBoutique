using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.CollectionExtensions;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueConsole.Factories.Services;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base;
using BoutiqueDTO.Models.Interfaces.RestClients;
using BoutiqueLoader.Factories.DatabaseInitialize.Boutique;
using BoutiqueLoader.Models.Enums;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

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
        private const int POST_CHUNK = 2;

        /// <summary>
        /// Загрузить данные в базу
        /// </summary>
        public static async Task<IResultError> PostData(IRestHttpClient restClientBoutique, IBoutiqueLogger boutiqueLogger) =>
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
        private static async Task<IResultError> GenderUpload(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await ClothesRestServiceFactory.GetGenderRestService(restClient).
            MapAsync(service => ServicePostAction(service, GenderInitialize.Genders, boutiqueLogger));

        /// <summary>
        /// Загрузить категории одежды в базу
        /// </summary>
        private static async Task<IResultError> CategoryUpload(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await ClothesRestServiceFactory.GetCategoryRestService(restClient).
            MapAsync(service => ServicePostAction(service, CategoryInitialize.Categories, boutiqueLogger));

        /// <summary>
        /// Загрузить цвет одежды в базу
        /// </summary>
        private static async Task<IResultError> ColorUpload(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await ClothesRestServiceFactory.GetColorRestService(restClient).
            MapAsync(service => ServicePostAction(service, ColorInitialize.ColorClothes, boutiqueLogger));

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> ClothesTypeUpload(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await ClothesRestServiceFactory.GetClothesTypeRestService(restClient).
            MapAsync(service => ServicePostAction(service, ClothesTypeInitialize.ClothesTypeMains, boutiqueLogger));

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> SizeUpload(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await ClothesRestServiceFactory.GetSizeRestService(restClient).
            MapAsync(service => ServicePostAction(service, SizeInitialize.Sizes, boutiqueLogger));

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> SizeGroupUpload(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await ClothesRestServiceFactory.GetSizeGroupRestService(restClient).
            MapAsync(service => ServicePostAction(service, SizeGroupInitialize.SizeGroupMains, boutiqueLogger));

        /// <summary>
        /// Загрузить одежду в базу
        /// </summary>
        private static async Task<IResultError> ClothesUpload(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await ClothesRestServiceFactory.GetClothesRestService(restClient).
            MapAsync(service =>
                ClothesInitialize.ClothesMains.
                SelectChunk(POST_CHUNK).ToList().
                Map(clothesMainsChucked => clothesMainsChucked.
                                           Select((clothesMains, index) => ServicePostAction(service, clothesMains, boutiqueLogger,
                                                                                             index, clothesMainsChucked.Count - 1)).
                                           ToResultErrorsTaskAsync()));

        /// <summary>
        /// Логгирование загрузки
        /// </summary>
        private static async Task<IResultError> ServicePostAction<TId, TDomain>(IRestServiceBase<TId, TDomain> restService,
                                                                                IEnumerable<TDomain> domains, IBoutiqueLogger boutiqueLogger)
            where TDomain : IDomainModel<TId>
            where TId : notnull =>
            await restService.PostCollectionAsync(domains).
            VoidTaskAsync(result => BoutiqueServiceLog.LogServiceAction<TId, TDomain>(result, boutiqueLogger, ServiceActionType.Post));

        /// <summary>
        /// Логгирование загрузки
        /// </summary>
        private static async Task<IResultError> ServicePostAction<TId, TDomain>(IRestServiceBase<TId, TDomain> restService,
                                                                                IEnumerable<TDomain> domains, IBoutiqueLogger boutiqueLogger,
                                                                                int index, int indexMax)
            where TDomain : IDomainModel<TId>
            where TId : notnull =>
            await restService.PostCollectionAsync(domains).
            VoidTaskAsync(result => BoutiqueServiceLog.LogServiceAction<TId, TDomain>(result, boutiqueLogger, ServiceActionType.Post,
                                                                                      index, indexMax));
    }
}
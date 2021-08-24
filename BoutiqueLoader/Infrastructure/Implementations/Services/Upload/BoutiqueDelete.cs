using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base;
using BoutiqueDTO.Models.Interfaces.RestClients;
using BoutiqueLoader.Factories.Services;
using BoutiqueLoader.Models.Enums;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Interfaces.Result;

namespace BoutiqueLoader.Infrastructure.Implementations.Services.Upload
{
    /// <summary>
    /// Удаление данных
    /// </summary>
    public static class BoutiqueDelete
    {
        /// <summary>
        /// Загрузить данные в базу
        /// </summary>
        public static async Task<IResultError> DeleteData(IRestHttpClient httpClient, IBoutiqueLogger boutiqueLogger) =>
            await httpClient.ToResultValue().
            Void(_ => boutiqueLogger.ShowMessage("Начало удаления данных")).
            ResultValueBindErrorsOkAsync(restClient => SizeGroupDelete(restClient, boutiqueLogger)).
            ResultValueBindErrorsOkBindAsync(restClient => ClothesTypeDelete(restClient, boutiqueLogger)).
            ResultValueBindErrorsOkBindAsync(restClient => GenderDelete(restClient, boutiqueLogger)).
            ResultValueBindErrorsOkBindAsync(restClient => CategoryDelete(restClient, boutiqueLogger)).
            ResultValueBindErrorsOkBindAsync(restClient => ColorDelete(restClient, boutiqueLogger)).
            ResultValueBindErrorsOkBindAsync(restClient => SizeDelete(restClient, boutiqueLogger)).
            ResultValueBindErrorsOkBindAsync(restClient => ClothesDelete(restClient, boutiqueLogger)).
            VoidTaskAsync(_ => boutiqueLogger.ShowMessage("Удаление данных завершено"));

        /// <summary>
        /// Загрузить тип пола в базу
        /// </summary>
        private static async Task<IResultError> GenderDelete(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetGenderRestService(restClient).
            MapAsync(service => ServiceDeleteAction(service, boutiqueLogger));

        /// <summary>
        /// Загрузить категории одежды в базу
        /// </summary>
        private static async Task<IResultError> CategoryDelete(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetCategoryRestService(restClient).
            MapAsync(service => ServiceDeleteAction(service, boutiqueLogger));

        /// <summary>
        /// Загрузить цвет одежды в базу
        /// </summary>
        private static async Task<IResultError> ColorDelete(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetColorRestService(restClient).
            MapAsync(service => ServiceDeleteAction(service, boutiqueLogger));

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> ClothesTypeDelete(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetClothesTypeRestService(restClient).
            MapAsync(service => ServiceDeleteAction(service, boutiqueLogger));

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> SizeDelete(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetSizeRestService(restClient).
            MapAsync(service => ServiceDeleteAction(service, boutiqueLogger));

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> SizeGroupDelete(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetSizeGroupRestService(restClient).
            MapAsync(service => ServiceDeleteAction(service, boutiqueLogger));

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> ClothesDelete(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetClothesRestService(restClient).
            MapAsync(service => ServiceDeleteAction(service, boutiqueLogger));

        /// <summary>
        /// Логгирование удаления
        /// </summary>
        private static async Task<IResultError> ServiceDeleteAction<TId, TDomain>(IRestServiceBase<TId, TDomain> restService, IBoutiqueLogger boutiqueLogger)
             where TDomain : IDomainModel<TId>
             where TId : notnull =>
            await restService.DeleteAsync().
            VoidTaskAsync(result => BoutiqueServiceLog.LogServiceAction<TId, TDomain>(result, boutiqueLogger, ServiceActionType.Delete));
    }
}
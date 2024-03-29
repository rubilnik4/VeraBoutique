﻿using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueConsole.Factories.Services;
using BoutiqueConsole.Infrastructure.Implementations.Services;
using BoutiqueConsole.Models.Enums;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base;
using BoutiqueDTO.Models.Interfaces.RestClients;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

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
            await ClothesRestServiceFactory.GetGenderRestService(restClient).
            MapAsync(service => ServiceDeleteAction(service, boutiqueLogger));

        /// <summary>
        /// Загрузить категории одежды в базу
        /// </summary>
        private static async Task<IResultError> CategoryDelete(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await ClothesRestServiceFactory.GetCategoryRestService(restClient).
            MapAsync(service => ServiceDeleteAction(service, boutiqueLogger));

        /// <summary>
        /// Загрузить цвет одежды в базу
        /// </summary>
        private static async Task<IResultError> ColorDelete(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await ClothesRestServiceFactory.GetColorRestService(restClient).
            MapAsync(service => ServiceDeleteAction(service, boutiqueLogger));

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> ClothesTypeDelete(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await ClothesRestServiceFactory.GetClothesTypeRestService(restClient).
            MapAsync(service => ServiceDeleteAction(service, boutiqueLogger));

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> SizeDelete(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await ClothesRestServiceFactory.GetSizeRestService(restClient).
            MapAsync(service => ServiceDeleteAction(service, boutiqueLogger));

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> SizeGroupDelete(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await ClothesRestServiceFactory.GetSizeGroupRestService(restClient).
            MapAsync(service => ServiceDeleteAction(service, boutiqueLogger));

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> ClothesDelete(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await ClothesRestServiceFactory.GetClothesRestService(restClient).
            MapAsync(service => ServiceDeleteAction(service, boutiqueLogger));

        /// <summary>
        /// Логгирование удаления
        /// </summary>
        private static async Task<IResultError> ServiceDeleteAction<TId, TDomain>(IRestServiceBase<TId, TDomain> restService, IBoutiqueLogger boutiqueLogger)
             where TDomain : IDomainModel<TId>
             where TId : notnull =>
            await restService.DeleteAsync().
            VoidTaskAsync(result => BoutiqueServiceLogging.LogServiceAction<TId, TDomain>(result, boutiqueLogger, ServiceActionType.Delete));
    }
}
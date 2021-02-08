using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueLoader.Factories.Services;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;
using RestSharp;

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
        public static async Task<IResultError> DeleteData(IRestClient restClientBoutique, IBoutiqueLogger boutiqueLogger) =>
            await restClientBoutique.ToResultValue().
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
        private static async Task<IResultError> GenderDelete(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetGenderRestService(restClient).
            Delete();

        /// <summary>
        /// Загрузить категории одежды в базу
        /// </summary>
        private static async Task<IResultError> CategoryDelete(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetCategoryRestService(restClient).
            Delete();

        /// <summary>
        /// Загрузить цвет одежды в базу
        /// </summary>
        private static async Task<IResultError> ColorDelete(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetColorRestService(restClient).
            Delete();

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> ClothesTypeDelete(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetClothesTypeRestService(restClient).
            Delete();

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> SizeDelete(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetSizeRestService(restClient).
            Delete();

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> SizeGroupDelete(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetSizeGroupRestService(restClient).
            Delete();

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> ClothesDelete(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetClothesRestService(restClient).
            Delete();
    }
}
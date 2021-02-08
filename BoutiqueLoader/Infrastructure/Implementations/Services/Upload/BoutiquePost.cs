using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueLoader.Factories.DatabaseInitialize.Boutique;
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
    /// Загрузка данных
    /// </summary>
    public static class BoutiquePost
    {
        /// <summary>
        /// Загрузить данные в базу
        /// </summary>
        public static async Task<IResultError> PostData(IRestClient restClientBoutique, IBoutiqueLogger boutiqueLogger) =>
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
        private static async Task<IResultError> GenderUpload(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetGenderRestService(restClient).
            Post(GenderInitialize.Genders);

        /// <summary>
        /// Загрузить категории одежды в базу
        /// </summary>
        private static async Task<IResultError> CategoryUpload(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetCategoryRestService(restClient).
            Post(CategoryInitialize.Categories);

        /// <summary>
        /// Загрузить цвет одежды в базу
        /// </summary>
        private static async Task<IResultError> ColorUpload(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetColorRestService(restClient).
            Post(ColorInitialize.ColorClothes);

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> ClothesTypeUpload(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetClothesTypeRestService(restClient).
            Post(ClothesTypeInitialize.ClothesTypes);

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> SizeUpload(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetSizeRestService(restClient).
            Post(SizeInitialize.Sizes);

        /// <summary>
        /// Загрузить тип одежды в базу
        /// </summary>
        private static async Task<IResultError> SizeGroupUpload(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetSizeGroupRestService(restClient).
            Post(SizeGroupInitialize.SizeGroups);

        /// <summary>
        /// Загрузить одежду в базу
        /// </summary>
        private static async Task<IResultError> ClothesUpload(IRestClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueRestServiceFactory.GetClothesRestService(restClient).
            Post(ClothesInitialize.Clothes);
    }
}
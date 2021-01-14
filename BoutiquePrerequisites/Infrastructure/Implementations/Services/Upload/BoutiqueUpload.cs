using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiquePrerequisites.Factories.Services;
using BoutiquePrerequisites.Infrastructure.Implementations.Services.Authorization;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;

namespace BoutiquePrerequisites.Infrastructure.Implementations.Services.Upload
{
    /// <summary>
    /// Загрузка данных в базу
    /// </summary>
    public static class BoutiqueUpload
    {
        /// <summary>
        /// Авторизироваться и загрузить данные в базу с предварительной очисткой
        /// </summary>
        public static async Task<IResultError> UploadAuthorizeData(IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueAuthorizeService.AuthorizeJwt(boutiqueLogger).
            ResultValueBindOkTaskAsync(BoutiqueRestServiceFactory.GetBoutiqueRestClient).
            ResultValueBindErrorsOkBindAsync(restClient => BoutiqueDelete.DeleteData(restClient, boutiqueLogger)).
            ResultValueBindErrorsOkBindAsync(restClient => BoutiquePost.PostData(restClient, boutiqueLogger));
    }
}
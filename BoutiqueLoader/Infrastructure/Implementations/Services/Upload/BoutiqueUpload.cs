using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueLoader.Factories.Services;
using BoutiqueLoader.Infrastructure.Implementations.Services.Authorize;
using BoutiqueLoader.Models.Interfaces.Configuration;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;

namespace BoutiqueLoader.Infrastructure.Implementations.Services.Upload
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
            ResultValueBindOkBindAsync(token => BoutiqueRestServiceFactory.GetBoutiqueRestClient(token,boutiqueLogger)).
            ResultValueBindErrorsOkBindAsync(restClient => BoutiqueDelete.DeleteData(restClient, boutiqueLogger)).
            ResultValueBindErrorsOkBindAsync(restClient => BoutiquePost.PostData(restClient, boutiqueLogger));
    }
}
using System;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueConsole.Factories.Configuration;
using BoutiqueConsole.Factories.Services;
using BoutiqueConsole.Infrastructure.Implementations.Services;
using BoutiqueConsole.Infrastructure.Services.Authorize;
using BoutiqueDTO.Models.Interfaces.RestClients;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

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
        public static async Task<IResultError> Upload(IBoutiqueLogger boutiqueLogger) =>
            await AuthorizeBaseService.ToAuthorizeService(UploadFunc(boutiqueLogger), boutiqueLogger);
        ///// <summary>
        ///// Функция загрузки
        ///// </summary>
        private static Func<IRestHttpClient, Task<IResultValue<IRestHttpClient>>> UploadFunc(IBoutiqueLogger boutiqueLogger) =>
            httpClient => Upload(httpClient, boutiqueLogger);

        /// <summary>
        /// Загрузить данные в базу с предварительной очисткой
        /// </summary>
        private static async Task<IResultValue<IRestHttpClient>> Upload(IRestHttpClient restClient, IBoutiqueLogger boutiqueLogger) =>
            await restClient.ToResultValue().
            ResultValueBindErrorsOkAsync(_ => BoutiqueDelete.DeleteData(restClient, boutiqueLogger)).
            ResultValueBindErrorsOkBindAsync(_ => BoutiquePost.PostData(restClient, boutiqueLogger));
    }
}
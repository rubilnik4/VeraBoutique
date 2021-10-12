using System;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueConsole.Factories.Services;
using BoutiqueConsole.Infrastructure.Services.Authorize;
using BoutiqueDTO.Models.Interfaces.RestClients;
using BoutiqueLoader.Factories.Configuration;
using BoutiqueLoader.Models.Interfaces.Configuration;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
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
        public static async Task<IResultError> UploadAuthorizeData(IBoutiqueLogger boutiqueLogger) =>
            await UploadFunc(boutiqueLogger).
            ResultValueCurryOkAsync(LoaderConfigurationFactory.GetConfiguration(boutiqueLogger).
                                    ResultValueOkTaskAsync(config => config.HostConfiguration)).
            ResultValueCurryOkBindAsync(AuthorizeConfigurationFactory.GetConfiguration(boutiqueLogger)).
            ResultValueBindOkBindAsync(func => func());

        /// <summary>
        /// Функция авторизации
        /// </summary>
        private static IResultValue<Func<IHostConfigurationDomain, IAuthorizeDomain, Task<IResultValue<IRestHttpClient>>>> UploadFunc(IBoutiqueLogger boutiqueLogger) =>
            new ResultValue<Func<IHostConfigurationDomain, IAuthorizeDomain, Task<IResultValue<IRestHttpClient>>>>(
                (hostConfiguration, authorize) => Upload(hostConfiguration, authorize, boutiqueLogger));

        /// <summary>
        /// Загрузить данные в базу с предварительной очисткой
        /// </summary>
        private static async Task<IResultValue<IRestHttpClient>> Upload(IHostConfigurationDomain hostConfiguration,
                                                       IAuthorizeDomain authorize, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueAuthorizeService.AuthorizeJwt(boutiqueLogger, hostConfiguration, authorize).
            ResultValueBindErrorsOkBindAsync(restClient => BoutiqueDelete.DeleteData(restClient, boutiqueLogger)).
            ResultValueBindErrorsOkBindAsync(restClient => BoutiquePost.PostData(restClient, boutiqueLogger));
    }
}
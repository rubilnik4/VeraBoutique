using System;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueConsole.Factories.Configuration;
using BoutiqueConsole.Factories.Services;
using BoutiqueConsole.Infrastructure.Services.Authorize;
using BoutiqueDTO.Models.Interfaces.RestClients;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueUserManagement.Infrastructure.Implementations.Services
{
    /// <summary>
    /// Сервис удаления пользователей
    /// </summary>
    public static class BoutiqueUserService
    {
        /// <summary>
        /// Авторизироваться и загрузить данные в базу с предварительной очисткой
        /// </summary>
        public static async Task<IResultError> DeleteUsersAuthorize(IBoutiqueLogger boutiqueLogger) =>
            await DeleteUsersFunc(boutiqueLogger).
            ResultValueCurryOkAsync(LoaderConfigurationFactory.GetConfiguration(boutiqueLogger).
                                    ResultValueOkTaskAsync(config => config.HostConfiguration)).
            ResultValueCurryOkBindAsync(AuthorizeConfigurationFactory.GetConfiguration(boutiqueLogger)).
            ResultValueBindOkBindAsync(func => func());

        /// <summary>
        /// Функция авторизации
        /// </summary>
        private static IResultValue<Func<IHostConfigurationDomain, IAuthorizeDomain, Task<IResultValue<IRestHttpClient>>>> DeleteUsersFunc(IBoutiqueLogger boutiqueLogger) =>
            new ResultValue<Func<IHostConfigurationDomain, IAuthorizeDomain, Task<IResultValue<IRestHttpClient>>>>(
                (hostConfiguration, authorize) => DeleteUsers(hostConfiguration, authorize, boutiqueLogger));

        /// <summary>
        /// Авторизироваться и удалить пользователей
        /// </summary>
        private static async Task<IResultValue<IRestHttpClient>> DeleteUsers(IHostConfigurationDomain hostConfiguration, IAuthorizeDomain authorize, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueAuthorizeService.AuthorizeJwt(hostConfiguration, authorize, boutiqueLogger).
            ResultValueBindErrorsOkBindAsync(httpClient =>  BoutiqueDeleteUsers.DeleteData(httpClient, boutiqueLogger));
    }
}
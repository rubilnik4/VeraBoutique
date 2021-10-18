using System;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueConsole.Factories.Configuration;
using BoutiqueConsole.Factories.Services;
using BoutiqueConsole.Infrastructure.Implementations.Services;
using BoutiqueDTO.Models.Interfaces.RestClients;
using BoutiqueUserManagement.Models.Enums;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Implementations.Errors;
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
        /// Авторизироваться и выполнить операцию с пользователями
        /// </summary>
        public static async Task<IResultError> UserOperate(UserOperationType operationType, IBoutiqueLogger boutiqueLogger) =>
            await UsersFunc(operationType, boutiqueLogger).
            ResultValueCurryOkAsync(LoaderConfigurationFactory.GetConfiguration(boutiqueLogger).
                                    ResultValueOkTaskAsync(config => config.HostConfiguration)).
            ResultValueCurryOkBindAsync(AuthorizeConfigurationFactory.GetConfiguration(boutiqueLogger)).
            ResultValueBindOkBindAsync(func => func());

        /// <summary>
        /// Функция авторизации
        /// </summary>
        private static IResultValue<Func<IHostConfigurationDomain, IAuthorizeDomain, Task<IResultValue<IRestHttpClient>>>> UsersFunc(UserOperationType operationType,
                                                                                                                                           IBoutiqueLogger boutiqueLogger) =>
            new ResultValue<Func<IHostConfigurationDomain, IAuthorizeDomain, Task<IResultValue<IRestHttpClient>>>>(
                (hostConfiguration, authorize) => UserOperate(operationType, hostConfiguration, authorize, boutiqueLogger));

        /// <summary>
        /// Авторизироваться и выполнить операцию с пользователями
        /// </summary>
        private static async Task<IResultValue<IRestHttpClient>> UserOperate(UserOperationType userOperationType, IHostConfigurationDomain hostConfiguration,
                                                                             IAuthorizeDomain authorize, IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueAuthorizeService.AuthorizeJwt(hostConfiguration, authorize, boutiqueLogger).
            ResultValueBindErrorsOkBindAsync(httpClient => GetUserOperation(userOperationType, httpClient, boutiqueLogger));

        /// <summary>
        /// Выполнить операцию с пользователями
        /// </summary>
        private static async Task<IResultError> GetUserOperation(UserOperationType operationType, IRestHttpClient httpClient,
                                                                 IBoutiqueLogger boutiqueLogger) =>
            operationType switch
            {
                UserOperationType.Read => await BoutiqueGetUsers.GetUsers(httpClient, boutiqueLogger),
                UserOperationType.Delete => await BoutiqueDeleteUsers.DeleteUsers(httpClient, boutiqueLogger),
                var type => ErrorResultFactory.ValueNotValidError(operationType, typeof(BoutiqueUserService), $"Отсутствует тип данных {type}").ToResultError(),
            };
    }
}
using System;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueConsole.Factories.Configuration;
using BoutiqueConsole.Infrastructure.Services.Authorize;
using BoutiqueDTO.Models.Interfaces.RestClients;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueConsole.Infrastructure.Implementations.Services
{
    public static class AuthorizeBaseService
    {
        /// <summary>
        /// Авторизироваться и обратиться к сервису
        /// </summary>
        public static async Task<IResultError> ToAuthorizeService(Func<IRestHttpClient, Task<IResultValue<IRestHttpClient>>> serviceFunc, IBoutiqueLogger boutiqueLogger) =>
            await ServiceFunc(serviceFunc, boutiqueLogger).
            ResultValueCurryOkAsync(LoaderConfigurationFactory.GetConfiguration(boutiqueLogger).
                                    ResultValueOkTaskAsync(config => config.HostConfiguration)).
            ResultValueCurryOkBindAsync(AuthorizeConfigurationFactory.GetConfiguration(boutiqueLogger)).
            ResultValueBindOkBindAsync(func => func());

        /// <summary>
        /// Функция авторизации
        /// </summary>
        private static IResultValue<Func<IHostConfigurationDomain, IAuthorizeDomain, Task<IResultValue<IRestHttpClient>>>> ServiceFunc(Func<IRestHttpClient, Task<IResultValue<IRestHttpClient>>> serviceFunc,
                                                                                                                                       IBoutiqueLogger boutiqueLogger) =>
            new ResultValue<Func<IHostConfigurationDomain, IAuthorizeDomain, Task<IResultValue<IRestHttpClient>>>>(
                (hostConfiguration, authorize) => BoutiqueAuthorizeService.AuthorizeJwt(hostConfiguration, authorize, boutiqueLogger).
                                                  ResultValueBindOkBindAsync(serviceFunc));
    }
}
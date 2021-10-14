using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueConsole.Infrastructure.Implementations.Configuration;
using BoutiqueConsole.Infrastructure.Interfaces.Configuration;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Identity;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueConsole.Factories.Configuration
{
    /// <summary>
    /// Фабрика для создания доступа к файлам авторизации консольного приложения
    /// </summary>
    public static class AuthorizeConfigurationFactory
    {
        /// <summary>
        /// Получить конфигурацию авторизации
        /// </summary>
        public static async Task<IResultValue<IAuthorizeDomain>> GetConfiguration(IBoutiqueLogger boutiqueLogger) =>
            await AuthorizeConfigurationManager.GetConfigurationAsync().
            ResultValueVoidBadTaskAsync(errors => boutiqueLogger.
                                                  Void(_ => boutiqueLogger.ShowMessage("Ошибка файла авторизации")).
                                                  Void(_ => boutiqueLogger.ShowErrors(errors)));

        /// <summary>
        /// Создать доступа к файлам авторизации консольного приложения
        /// </summary>
        private static IAuthorizeConfigurationManager AuthorizeConfigurationManager =>
            new AuthorizeTransferConverter().
            Map(authorizeTransferConverter => new AuthorizeConfigurationManager(authorizeTransferConverter));
    }
}
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Authorization;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Configuration;
using BoutiqueLoader.Infrastructure.Implementations.Configuration;
using BoutiqueLoader.Infrastructure.Implementations.Converters;
using BoutiqueLoader.Infrastructure.Interfaces.Configuration;
using BoutiqueLoader.Models.Interfaces.Configuration;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Results;

namespace BoutiqueLoader.Factories.Configuration
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
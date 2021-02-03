using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Configuration;
using BoutiqueLoader.Infrastructure.Implementations.Configuration;
using BoutiqueLoader.Infrastructure.Implementations.Converters;
using BoutiqueLoader.Infrastructure.Interfaces.Configuration;
using BoutiqueLoader.Models.Interfaces.Configuration;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueLoader.Factories.Configuration
{
    /// <summary>
    /// Фабрика для создания доступа к файлам конфигурации консольного приложения
    /// </summary>
    public static class LoaderConfigurationFactory
    {
        /// <summary>
        /// Создать доступа к файлам конфигурации консольного приложения
        /// </summary>
        public static ILoaderConfigurationManager LoaderConfigurationManager =>
            new HostConfigurationTransferConverter().
            Map(hostConfigurationConverter => new LoaderConfigurationTransferConverter(hostConfigurationConverter)).
            Map(loaderConfigurationConverter =>new LoaderConfigurationManager(loaderConfigurationConverter));

        /// <summary>
        /// Получить конфигурацию
        /// </summary>
        public static async Task<IResultValue<ILoaderConfigurationDomain>> GetConfiguration(IBoutiqueLogger boutiqueLogger) =>
            await LoaderConfigurationManager.GetConfigurationAsync().
            ResultValueVoidBadTaskAsync(errors => boutiqueLogger.
                                                  Void(_ => boutiqueLogger.ShowMessage("Ошибка конфигурационного файла")).
                                                  Void(_ => boutiqueLogger.ShowErrors(errors)));
    }
}
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Configuration;
using BoutiqueLoader.Infrastructure.Implementations.Configuration;
using BoutiqueLoader.Infrastructure.Implementations.Converters;
using BoutiqueLoader.Infrastructure.Interfaces.Configuration;
using BoutiqueLoader.Models.Interfaces.Configuration;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueLoader.Factories.Configuration
{
    /// <summary>
    /// Фабрика для создания доступа к файлам конфигурации консольного приложения
    /// </summary>
    public static class LoaderConfigurationFactory
    {
        /// <summary>
        /// Получить конфигурацию
        /// </summary>
        public static async Task<IResultValue<ILoaderConfigurationDomain>> GetConfiguration(IBoutiqueLogger boutiqueLogger) =>
            await LoaderConfigurationManager.GetConfigurationAsync().
            ResultValueVoidBadTaskAsync(errors => boutiqueLogger.
                                                  Void(_ => boutiqueLogger.ShowMessage("Ошибка конфигурационного файла")).
                                                  Void(_ => boutiqueLogger.ShowErrors(errors)));

        /// <summary>
        /// Создать доступа к файлам конфигурации консольного приложения
        /// </summary>
        private static ILoaderConfigurationManager LoaderConfigurationManager =>
            new HostConfigurationTransferConverter().
            Map(hostConfigurationConverter => new LoaderConfigurationTransferConverter(hostConfigurationConverter)).
            Map(loaderConfigurationConverter => new LoaderConfigurationManager(loaderConfigurationConverter));
    }
}
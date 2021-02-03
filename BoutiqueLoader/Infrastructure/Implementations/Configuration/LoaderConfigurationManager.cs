using System;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using BoutiqueDTO.Infrastructure.Implementations.Configuration;
using BoutiqueLoader.Infrastructure.Interfaces.Configuration;
using BoutiqueLoader.Infrastructure.Interfaces.Converters;
using BoutiqueLoader.Models.Implementations.Configuration;
using BoutiqueLoader.Models.Interfaces.Configuration;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueLoader.Infrastructure.Implementations.Configuration
{
    /// <summary>
    /// Доступ к файлам конфигурации консольного приложения
    /// </summary>
    public class LoaderConfigurationManager: ConfigurationManager<Guid, ILoaderConfigurationDomain, LoaderConfigurationTransfer>,
                                             ILoaderConfigurationManager
    {
        public LoaderConfigurationManager(ILoaderConfigurationTransferConverter loaderConfigurationTransferConverter)
          : base(loaderConfigurationTransferConverter)
        { }

        /// <summary>
        /// Имя файла конфигурации
        /// </summary>
        private const string CONFIGURATION_FILENAME = "appsettings.json";

        /// <summary>
        /// Получить конфигурацию в текстовом виде
        /// </summary>
        public override string GetConfigurationText() =>
            GetAssetText(CONFIGURATION_FILENAME);

        /// <summary>
        /// Получить конфигурацию в текстовом виде асинхронно
        /// </summary>
        public override async Task<string> GetConfigurationTextAsync() =>
            await GetAssetTextAsync(CONFIGURATION_FILENAME);

        /// <summary>
        /// Получить файл в текстовом формате
        /// </summary>
        private static string GetAssetText(string filename) =>
            GetConfigurationFilePath(filename).
            Map(File.ReadAllText);

        /// <summary>
        /// Получить файл в текстовом формате асинхронно
        /// </summary>
        private static async Task<string> GetAssetTextAsync(string filename) =>
            await GetConfigurationFilePath(filename).
            MapAsync(filePath => File.ReadAllTextAsync(filePath));

        /// <summary>
        /// Получить путь к файлу конфигурации
        /// </summary>
        private static string GetConfigurationFilePath(string filename) =>
            (Directory.GetParent(AppContext.BaseDirectory)?.FullName ?? String.Empty).
            Map(directory => Path.Combine(directory, filename));
    }
}
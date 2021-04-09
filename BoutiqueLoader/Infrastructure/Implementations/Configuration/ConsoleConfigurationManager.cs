using System;
using System.IO;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDTO.Infrastructure.Implementations.Configuration;
using BoutiqueDTO.Infrastructure.Interfaces.Configuration;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Interfaces.Base;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueLoader.Infrastructure.Implementations.Configuration
{
    public abstract class ConsoleConfigurationManager<TId, TDomain, TTransfer> : ConfigurationManager<TId, TDomain, TTransfer>
        where TDomain : IDomainModel<TId>
        where TTransfer : class, ITransferModel<TId>
        where TId : notnull
    {
        protected ConsoleConfigurationManager(ITransferConverter<TId, TDomain, TTransfer> configurationConverter) 
            : base(configurationConverter)
        { }

        /// <summary>
        /// Имя файла конфигурации
        /// </summary>
        protected abstract string ConfigurationFileName { get; }

        /// <summary>
        /// Получить конфигурацию в текстовом виде асинхронно
        /// </summary>
        public override async Task<string> GetConfigurationText() =>
            await GetAssetText(ConfigurationFileName);

        /// <summary>
        /// Получить файл в текстовом формате асинхронно
        /// </summary>
        private static async Task<string> GetAssetText(string filename) =>
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
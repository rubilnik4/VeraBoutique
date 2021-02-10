using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using BoutiqueDTO.Infrastructure.Implementations.Configuration;
using BoutiqueXamarin.Infrastructure.Interfaces.Configuration;
using BoutiqueXamarin.Infrastructure.Interfaces.Converters;
using BoutiqueXamarin.Models.Implementation.Configuration;
using BoutiqueXamarin.Models.Interfaces.Configuration;
using Newtonsoft.Json;
using Xamarin.Forms.Platform.Android;

namespace BoutiqueXamarin.Droid.Infrastructure.Implementation.Configuration
{
    /// <summary>
    /// Доступ к файлам конфигурации для андроида
    /// </summary>
    public class AndroidConfigurationManager : ConfigurationManager<Guid, IXamarinConfigurationDomain, XamarinConfigurationTransfer>,
                                               IXamarinConfigurationManager
    {
        public AndroidConfigurationManager(IXamarinConfigurationTransferConverter xamarinConfigurationTransferConverter)
            : base(xamarinConfigurationTransferConverter)
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
        private static string GetAssetText(string filename)
        {
            using var asset = Application.Context.Assets!.Open(filename);
            using var streamReader = new StreamReader(asset);
            return streamReader.ReadToEnd();
        }

        /// <summary>
        /// Получить файл в текстовом формате асинхронно
        /// </summary>
        private static async Task<string> GetAssetTextAsync(string filename)
        {
            await using var asset = Application.Context.Assets!.Open(filename);
            using var streamReader = new StreamReader(asset);
            return await streamReader.ReadToEndAsync();
        }
    }
}
using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using BoutiqueDTO.Infrastructure.Implementations.Configuration;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Configuration;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Converters;
using BoutiqueXamarinCommon.Models.Implementation.Configuration;
using BoutiqueXamarinCommon.Models.Interfaces.Configuration;
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
        private string ConfigurationFileName => $"appsettings{ConfigurationAdditional}.json";

        /// <summary>
        /// Параметры для дополнительных конфигураций
        /// </summary>
        private static string ConfigurationAdditional =>
#if DEVELOPMENT
            ".Development";
#elif TEST
            ".Test";
#else
            String.Empty;
#endif

        /// <summary>
        /// Получить конфигурацию в текстовом виде асинхронно
        /// </summary>
        public override string GetConfigurationText() =>
            GetAssetText(ConfigurationFileName);

        /// <summary>
        /// Получить файл в текстовом формате асинхронно
        /// </summary>
        private static string GetAssetText(string filename)
        {
            using var asset = Application.Context.Assets!.Open(filename);
            using var streamReader = new StreamReader(asset!);
            return streamReader.ReadToEnd();
        }
    }
}
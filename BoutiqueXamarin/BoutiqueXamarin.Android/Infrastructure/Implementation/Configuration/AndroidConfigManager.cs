using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using BoutiqueXamarin.Infrastructure.Implementations.Configuration;
using Newtonsoft.Json;
using Xamarin.Forms.Platform.Android;

namespace BoutiqueXamarin.Droid.Infrastructure.Implementation.Configuration
{
    /// <summary>
    /// Доступ к файлам для андроида
    /// </summary>
    public class AndroidConfigManager : ConfigManager
    {
        public override string GetConfiguration()
        {
            string platformFile = GetText("appsettings.json");
            return platformFile;
            //var platform = JsonConvert.DeserializeObject<Platform>(platformFile);

            //var configurationFile = await fileStorage.ReadAsString("config.json");

            //var configuration = JsonConvert.DeserializeObject<Definition.Configuration>(configurationFile);

            //configuration.Platform = platform;

            //return configuration;
        }

        /// <summary>
        /// Получить файл в текстовом формате
        /// </summary>
        private static string GetText(string filename)
        {
            using var asset = Application.Context.Assets!.Open(filename);
            using var streamReader = new StreamReader(asset);
            return streamReader.ReadToEnd();
        }
    }
}
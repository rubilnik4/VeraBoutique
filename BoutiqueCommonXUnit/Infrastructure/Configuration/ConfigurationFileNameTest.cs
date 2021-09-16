using BoutiqueCommon.Infrastructure.Implementation.Configuration;
using Xunit;

namespace BoutiqueCommonXUnit.Infrastructure.Configuration
{
    /// <summary>
    /// Имя файла конфигурации. Тесты
    /// </summary>
    public class ConfigurationFileNameTest
    {
        /// <summary>
        /// Файл конфигурации с неизвестной сборкой
        /// </summary>
        [Fact]
        public void ConfigurationFileName_Empty()
        {
            const string fileSettingsName = "fileSettingsName";
            const string configuration = "";

            string jsonName = ConfigurationFileNaming.GetJsonConfigurationName(fileSettingsName, configuration);

            Assert.Equal($"{fileSettingsName}.json", jsonName);
        }

        /// <summary>
        /// Файл конфигурации с неизвестной сборкой
        /// </summary>
        [Fact]
        public void ConfigurationFileName_Empty_NoParameter()
        {
            const string fileSettingsName = "fileSettingsName";

            string jsonName = ConfigurationFileNaming.GetJsonConfigurationName(fileSettingsName);

            Assert.Equal($"{fileSettingsName}.json", jsonName);
        }

        /// <summary>
        /// Файл конфигурации в разработке
        /// </summary>
        [Fact]
        public void ConfigurationFileName_Development()
        {
            const string fileSettingsName = "fileSettingsName";
            const string configuration = "Development";

            string jsonName = ConfigurationFileNaming.GetJsonConfigurationName(fileSettingsName, configuration);

            Assert.Equal($"{fileSettingsName}.{configuration}.json", jsonName);
        }
    }
}
using System;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueCommon.Infrastructure.Implementation.Configuration
{
    /// <summary>
    /// Имя файла конфигурации
    /// </summary>
    public static class ConfigurationFileNaming
    {
        /// <summary>
        /// Получить имя конфигурации файла
        /// </summary>
        public static string GetJsonConfigurationName(string fileSettingsName) =>
            GetJsonConfigurationName(fileSettingsName, String.Empty);

        /// <summary>
        /// Получить имя конфигурации файла
        /// </summary>
        public static string GetJsonConfigurationName(string fileSettingsName, string configuration) =>
            configuration.
            WhereContinue(String.IsNullOrWhiteSpace,
                okFunc: _ => fileSettingsName,
                badFunc: config => $"{fileSettingsName}.{config}").
            Map(fileName => $"{fileName}.json");
    }
}
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using BoutiqueCommon.Infrastructure.Implementation.Configuration;
using Functional.FunctionalExtensions.Sync;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace BoutiquePrerequisites.Factories.Configuration
{
    /// <summary>
    /// Получение параметров проекта
    /// </summary>
    public static class ConfigurationFactory
    {
        /// <summary>
        /// Имя файла с параметрами
        /// </summary>
        private const string FILE_SETTINGS_NAME = "appsettings";

        /// <summary>
        /// Имя файла с логином и паролем
        /// </summary>
        private const string FILE_LOGIN_NAME = "loginsettings";

        /// <summary>
        /// Стадия разработки
        /// </summary>
        private const string DEVELOPMENT = "Development";

        /// <summary>
        /// Стадия тестирования
        /// </summary>
        private const string TEST = "Test";

        /// <summary>
        /// Параметры проекта
        /// </summary>
        public static IConfiguration Configuration { get; } = GetConfiguration();

        /// <summary>
        /// Параметры проекта
        /// </summary>
        private static IConfiguration GetConfiguration() =>
            new ConfigurationBuilder().
            SetBasePath(Directory.GetParent(AppContext.BaseDirectory)?.FullName).
            AddJsonFile(ConfigurationFileNaming.GetJsonConfigurationName(FILE_SETTINGS_NAME), false).
            Void(configurationBuilder => AddDevelopmentSettings(configurationBuilder)).
            Void(configurationBuilder => AddTestSettings(configurationBuilder)).
            AddJsonFile(ConfigurationFileNaming.GetJsonConfigurationName(FILE_SETTINGS_NAME, TEST), true).
            AddJsonFile(ConfigurationFileNaming.GetJsonConfigurationName(FILE_LOGIN_NAME), false).
            Build();

        /// <summary>
        /// Добавить параметры разработки
        /// </summary>
        [Conditional(DEVELOPMENT)]
        private static void AddDevelopmentSettings(IConfigurationBuilder configurationBuilder) =>
            configurationBuilder.
            AddJsonFile(ConfigurationFileNaming.GetJsonConfigurationName(FILE_SETTINGS_NAME, DEVELOPMENT), true);

        /// <summary>
        /// Добавить параметры тестирования
        /// </summary>
        [Conditional(TEST)]
        private static void AddTestSettings(IConfigurationBuilder configurationBuilder) =>
           configurationBuilder.
           AddJsonFile(ConfigurationFileNaming.GetJsonConfigurationName(FILE_SETTINGS_NAME, TEST), true);
    }
}
using System;
using System.IO;
using Microsoft.Extensions.Configuration;

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
        private const string FILE_SETTINGS_NAME = "appsettings.json";
        
        /// <summary>
        /// Параметры проекта
        /// </summary>
        public static IConfiguration Configuration =>
            new ConfigurationBuilder().
            SetBasePath(Directory.GetParent(AppContext.BaseDirectory)?.FullName).
            AddJsonFile(FILE_SETTINGS_NAME, false).
            Build();
    }
}
using System.Threading.Tasks;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Configuration
{
    /// <summary>
    /// Доступ к файлам
    /// </summary>
    public interface IConfigManager
    {
        /// <summary>
        /// Получить конфигурацию в текстовом виде
        /// </summary>
        string GetConfigurationText();

        /// <summary>
        /// Получить конфигурацию в текстовом виде асинхронно
        /// </summary>
        Task<string> GetConfigurationTextAsync();

        /// <summary>
        /// Получить конфигурацию в текстовом виде
        /// </summary>
        string GetConfiguration();

        /// <summary>
        /// Получить конфигурацию в текстовом виде асинхронно
        /// </summary>
        Task<string> GetConfigurationAsync();
    }
}
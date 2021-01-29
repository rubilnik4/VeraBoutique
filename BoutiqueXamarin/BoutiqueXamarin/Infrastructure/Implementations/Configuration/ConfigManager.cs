using System.Text;
using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Interfaces;
using BoutiqueXamarin.Infrastructure.Interfaces.Configuration;
using Functional.FunctionalExtensions.Async;

namespace BoutiqueXamarin.Infrastructure.Implementations.Configuration
{
    /// <summary>
    /// Доступ к файлам
    /// </summary>
    public abstract class ConfigManager: IConfigManager
    {
        /// <summary>
        /// Конфигурация
        /// </summary>
        public abstract string GetConfiguration();
    }
}
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueLoader.Infrastructure.Implementations.Logger;

namespace BoutiqueLoader.Factories.Logger
{
    /// <summary>
    /// Фабрика создания логгера
    /// </summary>
    public static class LoggerFactory
    {
        /// <summary>
        /// Логгер
        /// </summary>
        public static IBoutiqueLogger BoutiqueLogger =>
           new ConsoleBoutiqueLogger();
    }
}
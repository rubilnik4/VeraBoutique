using System;
using System.Threading.Tasks;
using NLog;
using NLog.Web;

namespace BoutiqueMVC.DependencyInjection
{
    /// <summary>
    /// Запуск логгера NLog
    /// </summary>
    public static class NLogStartUp
    {
        /// <summary>
        /// Конфигурационный файл логгера
        /// </summary>
        private const string NLOG_CONFIG = "nlog.config";

        /// <summary>
        /// Запустить процесс логгирования
        /// </summary>
        public static async Task NLogStart(Func<Task> startUpFunc)
        {
            var logger = NLogBuilder.ConfigureNLog(NLOG_CONFIG).GetCurrentClassLogger();

            try
            {
                logger.Debug("Web server started");
                await startUpFunc();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Web server stopped because of exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}
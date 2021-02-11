using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Container;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Configuration;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using Microsoft.Extensions.Configuration;
using Prism.Ioc;
using Prism.Unity;
using Unity;

namespace BoutiqueXamarin.DependencyInjection
{
    /// <summary>
    /// Регистрация конфигурации
    /// </summary>
    public static class ConfigurationRegistration
    {
        /// <summary>
        /// Регистрация конфигурации
        /// </summary>
        public static async Task<IResultError> RegisterConfiguration(IBoutiqueContainer container) =>
            await container.Resolve<IXamarinConfigurationManager>().
            Map(manager => manager.GetConfigurationAsync()).
            ResultValueVoidOkTaskAsync(container.Register);
    }
}
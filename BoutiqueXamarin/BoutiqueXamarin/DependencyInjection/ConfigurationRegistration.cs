using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Container;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Configuration;
using BoutiqueXamarinCommon.Models.Interfaces.Configuration;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
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
        public static async Task<IResultValue<IXamarinConfigurationDomain>> RegisterConfiguration(IBoutiqueContainer container) =>
            await container.Resolve<IXamarinConfigurationManager>().
            MapAsync(manager => manager.GetConfiguration()).
            ResultValueVoidOkTaskAsync(container.Register);
    }
}
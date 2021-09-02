using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Container;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Configuration;
using BoutiqueXamarinCommon.Models.Interfaces.Configuration;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;
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
        public static IResultValue<IXamarinConfigurationDomain> RegisterConfiguration(IBoutiqueContainer container) =>
            container.Resolve<IXamarinConfigurationManager>().
            Map(manager => manager.GetConfiguration()).
            ResultValueVoidOk(container.Register);
    }
}
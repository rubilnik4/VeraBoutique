using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Container;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarinCommon.Models.Implementation;
using BoutiqueXamarinCommon.Models.Interfaces;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Interfaces.Result;

namespace BoutiqueXamarin.DependencyInjection
{
    /// <summary>
    /// Регистрация сервисов проекта
    /// </summary>
    public static class ProjectRegistration
    {
        /// <summary>
        /// Регистрация сервисов проекта
        /// </summary>
        public static IResultError RegisterProject(IBoutiqueContainer container) =>
            new BoutiqueXamarinProject().ToResultValue().
            ResultValueVoidOk(container.Register<IBoutiqueXamarinProject>);
    }
}
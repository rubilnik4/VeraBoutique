using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Container;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarinCommon.Models.Implementation;
using BoutiqueXamarinCommon.Models.Interfaces;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
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
            container.Resolve<IGenderRestService>().
            Map(genderService => genderService.Get()).
            ResultCollectionOkToValue(genders => new BoutiqueXamarinProject(genders)).
            ResultValueVoidOk(container.Register<IBoutiqueXamarinProject>);
    }
}
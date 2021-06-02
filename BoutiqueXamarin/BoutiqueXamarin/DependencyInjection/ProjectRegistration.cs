using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Container;
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
        public static async Task<IResultError> RegisterProject(IBoutiqueContainer container) =>
            await container.Resolve<IGenderRestService>().
            MapAsync(genderService => genderService.GetGenderCategories()).
            ResultCollectionOkToValueTaskAsync(genderCategories => new BoutiqueXamarinProject(genderCategories)).
            ResultValueVoidOkTaskAsync(container.Register<IBoutiqueXamarinProject>);
    }
}
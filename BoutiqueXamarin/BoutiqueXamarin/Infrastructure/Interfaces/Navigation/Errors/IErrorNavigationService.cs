using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.Models.Implementations.Navigation.Errors;
using Prism.Navigation;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Errors
{
    /// <summary>
    /// Сервис навигации к странице ошибок
    /// </summary>
    public interface IErrorNavigationService : IBaseNavigationService<ErrorNavigationOptions>
    {
        /// <summary>
        /// Перейти к странице
        /// </summary>
        Task<INavigationResult> NavigateTo(IReadOnlyCollection<IErrorResult> errors);
    }
}
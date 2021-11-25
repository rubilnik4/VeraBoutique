using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Navigation;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Navigation
{
    public interface IDefaultNavigationService: INavigationServiceFactory
    {
        /// <summary>
        /// К стартовой странице
        /// </summary>
        Task<INavigationResult> ToInitialPage();

        /// <summary>
        /// Перейти к странице ошибок
        /// </summary>
        Task<INavigationResult> ToErrorPage(IEnumerable<IErrorResult> errors, Func<Task<INavigationResult>> reloadFunc);
    }
}
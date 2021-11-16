using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using Prism.Navigation;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueXamarin.Models.Implementations.Navigation.Errors
{
    /// <summary>
    /// Параметры навигации к странице ошибок
    /// </summary>
    public class ErrorNavigationOptions : BaseNavigationOptions
    {
        public ErrorNavigationOptions(IEnumerable<IErrorResult> errors, Func<Task<INavigationResult>> reloadFunc)
        {
            Errors = errors.ToList();
            ReloadFunc = reloadFunc;
        }

        /// <summary>
        /// Ошибки
        /// </summary>
        public IReadOnlyCollection<IErrorResult> Errors { get; }

        /// <summary>
        /// Функция перезагрузки
        /// </summary>
        public Func<Task<INavigationResult>> ReloadFunc { get; }
    }
}
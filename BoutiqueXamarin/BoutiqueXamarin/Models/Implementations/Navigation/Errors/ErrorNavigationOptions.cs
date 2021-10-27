using System.Collections.Generic;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueXamarin.Models.Implementations.Navigation.Errors
{
    /// <summary>
    /// Параметры навигации к странице ошибок
    /// </summary>
    public class ErrorNavigationOptions : BaseNavigationOptions
    {
        public ErrorNavigationOptions(IReadOnlyCollection<IErrorResult> errors)
        {
            Errors = errors;
        }

        /// <summary>
        /// Ошибки
        /// </summary>
        public IReadOnlyCollection<IErrorResult> Errors { get; }
    }
}
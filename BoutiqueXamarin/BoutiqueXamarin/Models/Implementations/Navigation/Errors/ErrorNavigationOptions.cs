using System.Collections.Generic;
using System.Linq;
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
        public ErrorNavigationOptions(IEnumerable<IErrorResult> errors)
        {
            Errors = errors.ToList();
        }

        /// <summary>
        /// Ошибки
        /// </summary>
        public IReadOnlyCollection<IErrorResult> Errors { get; }
    }
}
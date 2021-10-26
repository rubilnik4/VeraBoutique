using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueXamarin.Models.Implementations.Navigation.Errors
{
    /// <summary>
    /// Параметры навигации к странице ошибок
    /// </summary>
    public class ErrorNavigationOptions : BaseNavigationOptions
    {
        public ErrorNavigationOptions(IResultError resultError)
        {
            ResultError = resultError;
        }

        /// <summary>
        /// Ошибки
        /// </summary>
        public IResultError ResultError { get; }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.Models.Implementations.Navigation.Errors;
using BoutiqueXamarin.ViewModels.Authorizes.AuthorizeViewModelItems;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.ClothesDetails.ClothesDetailViewModelItems;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using Prism.Navigation;
using ReactiveUI;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using Xamarin.Essentials;

namespace BoutiqueXamarin.ViewModels.Errors
{
    /// <summary>
    /// Ошибки
    /// </summary>
    public class ErrorViewModel : NavigationViewModel<ErrorNavigationOptions>
    {
        public ErrorViewModel(INavigationServiceFactory navigationServiceFactory)
         : base(navigationServiceFactory)
        {
            _error = GetError();
            ReloadCommand = ReactiveCommand.CreateFromTask<Unit, INavigationResult>(_ => navigationServiceFactory.NavigateBack());
        }

        /// <summary>
        /// Ошибки
        /// </summary>
        private readonly ObservableAsPropertyHelper<IErrorResult> _error;

        /// <summary>
        /// Ошибки
        /// </summary>
        public IErrorResult Error =>
            _error.Value;

        public ReactiveCommand<Unit, INavigationResult> ReloadCommand { get; }

        /// <summary>
        /// Получить модели детальной одежды
        /// </summary>
        private ObservableAsPropertyHelper<IErrorResult> GetError() =>
            this.WhenAnyValue(x => x.NavigationOptions).
                 WhereNotNull().
                 Select(result => result.Errors.FirstOrDefault() ?? ErrorResultFactory.SimpleErrorType("Неизвестная ошибка")).
                 ToProperty(this, nameof(Error));
    }
}
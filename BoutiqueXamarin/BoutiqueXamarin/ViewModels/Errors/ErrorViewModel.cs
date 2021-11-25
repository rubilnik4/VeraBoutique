using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
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
        public ErrorViewModel(IDefaultNavigationService defaultNavigationService)
         : base(defaultNavigationService)
        {
            _error = GetError();
            _reloadFunc = GetReloadFunc();
            ReloadCommand = ReactiveCommand.CreateFromTask<Unit, INavigationResult>(_ => ReloadFunc());
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

        /// <summary>
        /// Функция перезагрузки
        /// </summary>
        private readonly ObservableAsPropertyHelper<Func<Task<INavigationResult>>> _reloadFunc;

        /// <summary>
        /// Функция перезагрузки
        /// </summary>
        public Func<Task<INavigationResult>> ReloadFunc =>
            _reloadFunc.Value;

        /// <summary>
        /// Команда перезагрузки
        /// </summary>
        public ReactiveCommand<Unit, INavigationResult> ReloadCommand { get; }

        /// <summary>
        /// Получить модели детальной одежды
        /// </summary>
        private ObservableAsPropertyHelper<IErrorResult> GetError() =>
            this.WhenAnyValue(x => x.NavigationOptions).
                 WhereNotNull().
                 Select(result => result.Errors.FirstOrDefault() ?? ErrorResultFactory.SimpleErrorType("Неизвестная ошибка")).
                 ToProperty(this, nameof(Error));

        /// <summary>
        /// Получить функцию перезагрузки
        /// </summary>
        private ObservableAsPropertyHelper<Func<Task<INavigationResult>>> GetReloadFunc() =>
            this.WhenAnyValue(x => x.NavigationOptions).
                 WhereNotNull().
                 Select(result => result.ReloadFunc).
                 ToProperty(this, nameof(ReloadFunc));
    }
}
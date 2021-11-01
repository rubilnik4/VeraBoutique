using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Errors;
using BoutiqueXamarin.ViewModels.Errors;
using BoutiqueXamarin.Views.Errors;
using ReactiveUI;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueXamarin.ViewModels.Base
{
    /// <summary>
    /// Базовая модель с обработкой ошибок
    /// </summary>
    public abstract class NavigationErrorViewModel<TParameter> : NavigationViewModel<TParameter>
       where TParameter : BaseNavigationOptions
    {
        protected NavigationErrorViewModel(INavigationServiceFactory navigationServiceFactory)
            : base(navigationServiceFactory)
        {
            _navigationServiceFactory = navigationServiceFactory;
            this.WhenAnyObservable(Result)
        }

        /// <summary>
        /// Сервис навигации к странице ошибок
        /// </summary>
        private readonly INavigationServiceFactory _navigationServiceFactory;

        protected virtual IObservable<IResultError> Result =>
            Observable.Return(new ResultError());

        /// <summary>
        /// Проверить стартовые ошибки
        /// </summary>
        protected IObservable<IReadOnlyCollection<TValue>> ValidateErrorPage<TValue>(IObservable<IResultCollection<TValue>> resultCollection) =>
            resultCollection.
            WhereNotNull().
            Select(result => result.ToResultValue()).
            Map(ValidateErrorPage);

        /// <summary>
        /// Проверить стартовые ошибки
        /// </summary>
        protected IObservable<TValue> ValidateErrorPage<TValue>(IObservable<IResultValue<TValue>> resultValue) =>
            resultValue.
            WhereNotNull().
            SelectMany(RedirectToErrorPage);

        /// <summary>
        /// Перенаправить на страницу ошибок
        /// </summary>
        private async Task<TValue> RedirectToErrorPage<TValue>(IResultValue<TValue> resultValue) =>
            await resultValue.
            ResultValueVoidBadAsync(errors => _navigationServiceFactory.ToErrorPage(errors)).
            MapTaskAsync(result => result.Value);
    }
}
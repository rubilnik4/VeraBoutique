using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Errors;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using ReactiveUI;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueXamarin.ViewModels.Base
{
    /// <summary>
    /// Базовая модель с обработкой ошибок
    /// </summary>
    public abstract class NavigationErrorViewModel<TParameter, TNavigate> : NavigationViewModel<TParameter, TNavigate>
       where TParameter : BaseNavigationOptions
       where TNavigate : IBaseNavigationService<TParameter>
    {
        protected NavigationErrorViewModel(TNavigate navigateService, IErrorNavigationService errorNavigationService)
            : base(navigateService)
        {
            _errorNavigationService = errorNavigationService;
        }

        /// <summary>
        /// Сервис навигации к странице ошибок
        /// </summary>
        private readonly IErrorNavigationService _errorNavigationService;

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
            ResultValueVoidBadAsync(errors => _errorNavigationService.NavigateTo(errors)).
            MapTaskAsync(result => result.Value);
    }
}
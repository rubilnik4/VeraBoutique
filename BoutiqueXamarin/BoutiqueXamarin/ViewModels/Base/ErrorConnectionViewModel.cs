using System;
using System.Reactive;
using System.Threading.Tasks;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Result;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Base
{
    /// <summary>
    /// Ошибки соединения
    /// </summary>
    public class ErrorConnectionViewModel : BaseViewModel
    {
        public ErrorConnectionViewModel(IResultError resultError, Func<Unit> reloadFunc)
        {
            ResultError = resultError;
            ReloadCommand = ReactiveCommand.Create(reloadFunc);
        }

        /// <summary>
        /// Ошибки
        /// </summary>
        public IResultError ResultError { get; }

        /// <summary>
        /// Команда перезагрузки
        /// </summary>
        public ReactiveCommand<Unit, Unit> ReloadCommand { get; }

        /// <summary>
        /// Создать модель без ошибок
        /// </summary>
        public static ErrorConnectionViewModel EmptyErrorConnectionViewModel =>
            new ErrorConnectionViewModel(new ResultError(), () => Unit.Default);
    }
}
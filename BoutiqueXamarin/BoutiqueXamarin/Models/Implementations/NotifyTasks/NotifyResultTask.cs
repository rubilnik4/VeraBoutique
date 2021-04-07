using System;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using Prism.Mvvm;

namespace BoutiqueXamarin.Models.Implementations.NotifyTasks
{
    /// <summary>
    /// Класс асинхронного процесса с обновлением интерфейса
    /// </summary>
    public class NotifyResultTask<T> : BindableBase
        where T : notnull
    {
        public NotifyResultTask(Func<Task<IResultValue<T>>> resultValueTask)
        {
            resultValueTask.Invoke();
           //_resultValueTask = resultValueTask;
            //WaitResultValue().ConfigureAwait(false);
        }

        /// <summary>
        /// Результирующий объекта задачи
        /// </summary>
        private readonly Task<IResultValue<T>> _resultValueTask;

        /// <summary>
        /// Значение при успешном завершении
        /// </summary>
        public T OkResult => 
            _resultValueTask.Result.Value ;

        /// <summary>
        /// Статус задачи
        /// </summary>
        public TaskStatus Status =>
            _resultValueTask.Status;

        /// <summary>
        /// Завершена ли задача
        /// </summary>
        public bool IsCompleted =>
            _resultValueTask.IsCompleted;

        /// <summary>
        /// Завершена ли задача успешно
        /// </summary>
        public bool OkStatus =>
            _resultValueTask.IsCompletedSuccessfully && _resultValueTask.Result.OkStatus;

        /// <summary>
        /// Завершена ли задача
        /// </summary>
        public bool HasErrors =>
            _resultValueTask.IsFaulted ||
            _resultValueTask.IsCanceled ||
            _resultValueTask.IsCompletedSuccessfully && _resultValueTask.Result.HasErrors;

        /// <summary>
        /// Запуск задачи с обновлением полей
        /// </summary>
        private async Task WaitResultValue() =>
            await _resultValueTask.
            VoidTaskAsync(_ => RaisePropertyChanged(nameof(Status))).
            VoidTaskAsync(_ => RaisePropertyChanged(nameof(IsCompleted))).
            VoidTaskAsync(_ => RaisePropertyChanged(nameof(OkStatus))).
            VoidTaskAsync(_ => RaisePropertyChanged(nameof(HasErrors))).
            VoidTaskAsync(_ => RaisePropertyChanged(nameof(OkResult)));
    }
}
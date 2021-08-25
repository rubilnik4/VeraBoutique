using System;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Results;
using Prism.Mvvm;

namespace BoutiqueXamarin.Models.Implementations.NotifyTasks
{
    /// <summary>
    /// Класс асинхронного процесса с обновлением интерфейса
    /// </summary>
    public class NotifyResultTask<T> : BindableBase
        where T : notnull
    {
        public NotifyResultTask(Task<IResultValue<T>> resultValueTask, T defaultValue)
        {
            _resultValueTask = resultValueTask;
            _defaultValue = defaultValue;
            WaitResultValue(resultValueTask).ConfigureAwait(false);
        }

        /// <summary>
        /// Результирующий объекта задачи
        /// </summary>
        private readonly Task<IResultValue<T>> _resultValueTask;

        /// <summary>
        /// Значение по умолчанию
        /// </summary>
        private readonly T _defaultValue;

        /// <summary>
        /// Значение при успешном завершении
        /// </summary>
        public T OkResult =>
            IsCompleted ? _resultValueTask.Result.Value : _defaultValue;

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
        private async Task WaitResultValue(Task<IResultValue<T>> resultValueTask) =>
            await resultValueTask.
            VoidTaskAsync(_ => RaisePropertyChanged(nameof(Status))).
            VoidTaskAsync(_ => RaisePropertyChanged(nameof(IsCompleted))).
            VoidTaskAsync(_ => RaisePropertyChanged(nameof(OkStatus))).
            VoidTaskAsync(_ => RaisePropertyChanged(nameof(HasErrors))).
            VoidTaskAsync(_ => RaisePropertyChanged(nameof(OkResult)));
    }
}
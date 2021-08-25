using System;
using System.Threading.Tasks;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Results;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultErrors
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно
    /// </summary>
    public static class ResultErrorTryAsyncExtensions
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static async Task<IResultError> ResultErrorTryAsync(Func<Task> action, IErrorResult errorType)
        {
            try
            {
                await action.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultError(errorType.AppendException(ex));
            }

            return new ResultError();
        }
    }
}
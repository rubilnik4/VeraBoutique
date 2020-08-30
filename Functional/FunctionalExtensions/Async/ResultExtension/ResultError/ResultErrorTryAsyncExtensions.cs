using System;
using System.Threading.Tasks;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultError
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно
    /// </summary>
    public static class ResultErrorTryAsyncExtensions
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static async Task<IResultError> ResultErrorTryAsync(Func<Task> action, Func<Exception, IErrorResult> tryFunc)
        {
            try
            {
                await action.Invoke();
            }
            catch (Exception ex)
            {
                return new Models.Implementations.Result.ResultError(tryFunc(ex));
            }

            return new Models.Implementations.Result.ResultError();
        }
    }
}
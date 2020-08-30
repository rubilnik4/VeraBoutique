using System;
using Functional.Models.Interfaces.Result;


namespace Functional.FunctionalExtensions.Sync.ResultExtension.ResultError
{
    /// <summary>
    /// Методы расширения для результирующего ответа и обработкой исключений
    /// </summary>
    public static class ResultErrorTryExtensions
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ или ошибку исключения
        /// </summary>
        public static IResultError ResultErrorTry(Action action, Func<Exception, IErrorResult> tryFunc)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                return new Models.Implementations.Result.ResultError(tryFunc(ex));
            }

            return new Models.Implementations.Result.ResultError();
        }
    }
}
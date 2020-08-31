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
        public static IResultError ResultErrorTry(Action action, IErrorResult error)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                return new Models.Implementations.Result.ResultError(error.AppendException(ex));
            }

            return new Models.Implementations.Result.ResultError();
        }
    }
}